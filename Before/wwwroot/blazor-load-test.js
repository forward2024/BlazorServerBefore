import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    vus: 2000, // Кількість одночасних віртуальних користувачів
    duration: '1m', // Тривалість тесту
};

export default function () {
    // Змініть на відповідний URL вашого Blazor-сервера
    const response = http.get('https://localhost:7186/');

    // Перевірте відповідь від сервера
    if (response.status !== 200) {
        console.log('Error: ' + response.status);
    }

    // Імітування користувача, який робить певні дії на сайті
    sleep(1);
}
