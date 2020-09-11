const KEY = 'Shopify_Cart';
export {
  save,
  get,
  remove
}

function save(cartData) {
  localStorage.setItem(KEY, JSON.stringify(cartData));
}

function get() {
  let cartData = {};
  const value = localStorage.getItem(KEY);

  if (value) {
    cartData = JSON.parse(value);
  }

  return cartData;
}

function remove() {
  localStorage.removeItem(KEY);
}
