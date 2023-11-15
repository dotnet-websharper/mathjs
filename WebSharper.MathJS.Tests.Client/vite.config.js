import fs from 'fs/promises';
import { defineConfig } from 'vite';
import http from 'https';

export default defineConfig(() => ({
    server: {
        proxy: {
            '/Api': {
                target: 'http://localhost:5000',
                changeOrigin: true,
                secure: false,
                agent: new http.Agent()                
            }
        },
        https: false
    }
}))