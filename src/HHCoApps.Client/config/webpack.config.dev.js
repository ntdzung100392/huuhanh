const path = require("path");
const merge = require('webpack-merge');
const baseConfig = require('./webpack.config.base.js');

module.exports = merge(baseConfig, {
  output: {
    path: path.resolve(__dirname, "../../DuluxGroup.CMSWeb/scripts"),
    filename: "app.js"
  }
});