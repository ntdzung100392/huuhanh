const path = require("path");
const merge = require('webpack-merge');
const baseConfig = require('./webpack.config.base.js');

module.exports = merge(baseConfig, {
  mode: "production",
  output: {
    path: path.resolve(__dirname, "../../DuluxGroup.CMSWeb/scripts"),
    filename: "app.min.js"
  },
  externals: {
    "react": "React",
    "react-dom": "ReactDOM",
    "react-bootstrap": "ReactBootstrap",
    "axios": "axios"
  }
});