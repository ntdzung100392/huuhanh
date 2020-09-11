class UrlHelper {
  static slugify(url) {
    if (url) {
      return url.toLowerCase().replace(/[^\w ]+/g, '').replace(/ +/g, '-');
    }

    return '';
  }
}

export default UrlHelper;