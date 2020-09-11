class ImageHelper {
  static getCropImageUrl(imageUrl, width, height, mode = 'crop') {
    if (imageUrl) {
      let resizeImageUrl = `${imageUrl}?width=${width}&heightratio=${height / width}&mode=${mode}`;
      const extension = imageUrl.split('.').slice(-1).pop();
      if (extension !== "png") {
        resizeImageUrl += "&bgcolor=fff";
      }

      if (AppConfig.isBrowserSupportWebP) {
        resizeImageUrl += "&format=webp";
      }

      return resizeImageUrl;
    }

    return AppConfig.images.noImageUrl;
  }
}

export default ImageHelper;