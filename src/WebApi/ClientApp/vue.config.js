module.exports = {
  outputDir: "../wwwroot/",
  filenameHashing: false,
  devServer: {
    proxy: {
      "^/api": {
        target: "https://localhost:5011",
        ws: false,
      },
    },
  },
  configureWebpack: {
    performance: {
      hints: false,
    },
    optimization: {
      splitChunks: {
        minSize: 10000,
        maxSize: 250000,
      },
    },
  },
};
