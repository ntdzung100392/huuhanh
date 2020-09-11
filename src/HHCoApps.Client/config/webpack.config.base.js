const mode = process.env.NODE_ENV || 'development';
const path = require("path");

module.exports = {
  entry: {
    app: ["core-js/stable", "./index.jsx"]
  },
  mode: mode,
  module: {
    rules: [
      {
        use: {
          loader: "babel-loader"
        },
        test: /\.jsx?$/,
        exclude: /node_modules/
      }
    ]
  },
  resolve: {
    alias: {
      '@': path.resolve('./'),
      'constants': path.resolve('./Constants'),
      'apis': path.resolve('./apis'),
      'components': path.resolve('./Components'),
      'commonComponents': path.resolve('./Components/common'),
      'helpers': path.resolve('./Helpers'),
      'services': path.resolve('./services')
    },
    extensions: ['.js', '.jsx']
  }
}