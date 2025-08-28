const config = {
  env: {
    apiEndpoint: import.meta.env.VITE_PUBLIC_API_ENDPOINT!,
    imageKit: {
      publicKey: import.meta.env.VITE_PUBLIC_IMAGEKIT_PUBLIC_KEY!,
      urlEndpoint: import.meta.env.VITE_PUBLIC_IMAGEKIT_URL_ENDPOINT!,
      privateKey: import.meta.env.VITE_IMAGEKIT_PRIVATE_KEY!,
    },
  },
};
export default config;
