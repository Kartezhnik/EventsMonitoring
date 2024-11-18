ИНСТРУКЦИЯ ПО ЗАПУСКУ ПРИЛОЖЕНИЯ EventsMonitoring
============================================================
1. Запускаем решение;
2. В браузере переходим по https://localhost:7003/index.html
(Открывается Swagger);
3. В Swagger выбираем нужное действие и нажимаем try it out;
4. Вводим нужные значения в поля;

Например: 

В /api/Auth/register в поле запроса:

{
 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "email": "string",
  "password": "string",
  "birthdayDate": "2024-11-18T05:22:49.503Z",
  "registrationDate": "2024-11-18T05:22:49.503Z",
  "eventInfoKey": null,
  "event": null
}

5. Нажимаем Execute;
6. Видим поле ответа

Например:

{
  "success": true,
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiZXhwIjoxNzMxOTA3OTgxLCJpc3MiOiJBdXRoU2VydmVyIiwiYXVkIjoiQXV0aENsaWVudCJ9.tTTT5Pk37PptMPEpWv-JmE0TLP-PgTmhwz5YTPiKNJw",
  "refreshToken": "81303dc3-3fcb-4007-912e-8a6ba34d585a"
}

