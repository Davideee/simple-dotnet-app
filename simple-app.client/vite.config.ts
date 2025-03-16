import { defineConfig } from 'vite';
import { fileURLToPath, URL } from 'node:url';
import plugin from '@vitejs/plugin-vue';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [plugin()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    proxy: {
      '^/api/': {
        target: process.env.VITE_ASPNETCORE_HTTPS_PORT ?
          `https://localhost:${process.env.VITE_ASPNETCORE_HTTPS_PORT}` :
          'https://localhost:5065',
        changeOrigin: true,
        secure: false,  // Nur für lokale Entwicklung, in der Produktion müsste das `true` sein.
        rewrite: (path) => path.replace(/^\/api/, ''),  // API-Pfad so umschreiben, dass die Anfrage korrekt zum Backend geht
      },
    },
    port: 5173,
  }
});
