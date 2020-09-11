import {BehaviorSubject} from 'rxjs';
import * as storage from './storage';

export class ShopifyCart {
  constructor(shopifyClient) {
    this.checkoutResource = shopifyClient;
    this.subject = new BehaviorSubject(storage.get());
    this.observable = this.subject.asObservable();
  }

  addToCart(productVariantId) {
    const _updateQuantity = function (cartStorage, _productVariantId) {
      if (cartStorage.variants) {
        const itemIndex = cartStorage.variants.findIndex(item => item.variantId === _productVariantId);
        let quantity = 1;

        if (itemIndex >= 0) {
          quantity = cartStorage[itemIndex].quantity + 1;
          return;
        }

        cartStorage.variants.push({
          variantId: _productVariantId,
          quantity: quantity
        });
      } else {
        cartStorage.variants = [{
          variantId: productVariantId,
          quantity: 1
        }]
      }
    };

    return new Promise((resolve, reject) => {
      let cartStorage = storage.get();

      if (cartStorage.checkoutId) {
        if (!this.checkoutResource) {
          console.log('error', 'shopifyClient is not found');
          return;
        }

        this.fetchCheckout(cartStorage.checkoutId).then((cart) => {
          if (cart['order'] || !cartStorage.variants) {
            cartStorage = {
              checkoutId: '',
              variants: [{
                variantId: productVariantId,
                quantity: 1
              }]
            };
          } else {
            _updateQuantity(cartStorage, productVariantId);
          }

          storage.save(cartStorage);
          const item = storage.get();
          this.subject.next(item);
          resolve(item);
        }, () => {
          cartStorage.checkoutId = '';
          _updateQuantity(cartStorage, productVariantId);

          storage.save(cartStorage);
          const item = storage.get();
          this.subject.next(item);
          resolve(item);
        });
      } else {
        _updateQuantity(cartStorage, productVariantId);

        storage.save(cartStorage);
        const item = storage.get();
        this.subject.next(item);
        resolve(item);
      }
    });
  }

  getCheckout() {
    const self = this;
    const _createEmptyCheckout = function (cartStorage) {
      return new Promise(resolve => {
        self.createEmptyCheckout().then((result) => {
          cartStorage.checkoutId = result.data.checkoutCreate.checkout.id;
          storage.save(cartStorage);

          self.checkoutAddLineItems(cartStorage.checkoutId, cartStorage.variants).then(checkout => {
            resolve(checkout);
          })
        });
      });
    };

    return new Promise((resolve, reject) => {
      if (!this.checkoutResource) {
        return;
      }

      const cartStorage = storage.get();

      if (!cartStorage.variants || cartStorage.variants.length === 0) {
        reject('There is no item in cart');
        return;
      }

      if (cartStorage.checkoutId) {
        this.fetchCheckout(cartStorage.checkoutId).then((checkout) => {
          if (checkout) {
            if (checkout['order']) {
              storage.remove();
              this.subject.next({});
              reject('The order has been created successful');
            } else {
              resolve(checkout);
            }
          } else {
            _createEmptyCheckout(cartStorage).then((checkout) => {
              resolve(checkout);
            });
          }
        }, () => {
          _createEmptyCheckout(cartStorage).then((checkout) => {
            resolve(checkout);
          });
        });
      } else {
        _createEmptyCheckout(cartStorage).then((checkout) => {
          resolve(checkout);
        });
      }
    });
  }

  completeCheckout() {
    return new Promise((resolve, reject) => {
      this.getCheckout().then(shopifyCheckout => {
        if (shopifyCheckout['order']) {
          storage.remove();
          this.subject.next({});
          reject('The order has been created successful');
        } else {
          const cartStorage = storage.get();
          this.checkoutReplaceLineItems(cartStorage.checkoutId, cartStorage.variants).then((checkout) => {
            resolve(checkout);
          });
        }
      });
    });
  }

  fetchCheckout(checkoutId) {
    return this.checkoutResource.fetch(checkoutId);
  }

  createEmptyCheckout() {
    const input = this.checkoutResource.graphQLClient.variable('input', 'CheckoutCreateInput!');
    const mutation = this.checkoutResource.graphQLClient.mutation('CreateEmptyCheckout', [input], (root) => {
      root.add('checkoutCreate', {args: {input}}, (checkoutCreatePayload) => {
        checkoutCreatePayload.add('checkout', (checkout) => {
          checkout.add('id');
        });
      });
    });

    return this.checkoutResource.graphQLClient.send(mutation, {input: {}});
  }

  checkoutAddLineItems(id, lineItems) {
    return this.checkoutResource.addLineItems(id, lineItems);
  }

  checkoutReplaceLineItems(id, lineItems) {
    return this.checkoutResource.replaceLineItems(id, lineItems);
  }

  updateShippingAddress(id, shippingAddress) {
    return new Promise((resolve, reject) => {
      this.checkoutResource.updateShippingAddress(id, shippingAddress).then((checkout) => {
        resolve(checkout);
      }, (error) => {
        reject(error);
      });
    });
  }
}
