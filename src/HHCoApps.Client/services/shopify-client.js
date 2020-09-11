import * as es6Promise from 'es6-promise';

es6Promise.polyfill();

import 'isomorphic-fetch';
import Client from 'shopify-buy/index.unoptimized.umd';

export * from './shopify-cart';

export default class ShopifyClient {
  constructor(config) {
    this.client = new Client.buildClient({
      domain: config.domain,
      storefrontAccessToken: config.token,
      apiVersion: config.apiVerison || '2020-07'
    }, fetch);

    this.productSelectionSetBuilder = (product) => {
      product.add('handle');
      product.add('id');
      product.add('title');
      product.add('priceRange', (range) => {
        range.add('maxVariantPrice', (money) => {
          money.add('amount');
          money.add('currencyCode');
        });
        range.add('minVariantPrice', (money) => {
          money.add('amount');
          money.add('currencyCode');
        });
      });
      product.addConnection('variants', {args: {first: 100}}, (variant) => {
        variant.add('id');
        variant.add('sku');
        variant.add('price');
        variant.add('quantityAvailable');
      });
    };
  }

  getProducts(pageSize) {
    return new Promise((resolve) => {
      this.fetchQueryRootData('products', {
        first: pageSize || 12
      }, this.productSelectionSetBuilder, 'GetAllProducts').then((result) => {
        const products = result.data.products.edges.map((product) => product.node);
        resolve(products);
      });
    });
  }

  getProductVariantsByTitle(productTitle) {
    return new Promise((resolve) => {
      const titleSearch ='title:' + JSON.stringify(productTitle);
      this.fetchQueryRootData('products', {
        first: 1,
        query: titleSearch
      }, this.productSelectionSetBuilder).then((result) => {
        const variants = result && result.length > 0 ? result[0].node.variants.edges.map(edge => edge.node) : [];        
        resolve(variants);
      });
    });
  }

  searchProductByTitle(titleItems, pageSize) {
    return new Promise((resolve) => {
      const titleSearch = titleItems.map(item => 'title:' + JSON.stringify(item)).join(' OR ');

      this.fetchQueryRootData('products', {
        first: pageSize || 12,
        query: titleSearch
      }, this.productSelectionSetBuilder, 'SearchProductByTitle').then((result) => {
        const products = result.reduce((acc, item) => {
          const data = item.node;
          data.variants = item.node.variants.edges.map(edge => edge.node);
          acc.push(data);

          return acc;
        }, []);
        resolve(products);
      });
    });
  }

  fetchQueryRootData(connectionName, variables, selectionBuilder, operatorName) {
    let acc = [];
    const client = this.client;
    const after = client.graphQLClient.variable('after', 'String');

    return new Promise((resolve) => {
      function fetchData(products, cursor) {
        let promise = null;

        if (cursor) {
          const nextQuery = client.graphQLClient.query(operatorName, [after], (root) => {
            variables['after'] = after;
            root.addConnection(connectionName, {args: variables}, (model) => {
              selectionBuilder(model);
            });
          });
          promise = client.graphQLClient.send(nextQuery, {after: cursor});
        } else {
          const query = client.graphQLClient.query(operatorName, (root) => {
            root.addConnection(connectionName, {args: variables}, (model) => {
              selectionBuilder(model);
            });
          });
          promise = client.graphQLClient.send(query, variables);
        }

        promise.then((result) => {
          const model = result.data[connectionName] || {};
          acc = acc.concat(model.edges);

          if (model.pageInfo.hasNextPage) {
            fetchData(acc, acc[acc.length - 1].cursor);
          } else {
            resolve(acc);
          }
        });
      }

      fetchData(acc);
    })
  }
}
