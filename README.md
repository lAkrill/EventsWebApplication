# EventsWebApplication

## Описание
EventsWebApplication — это система для работы с событиями.
Проект еще будет дорабатываться, но в ветке main находится версия с работающим WebAPI.

## Установка и запуск

Перейдите в папку, в которую будет скачан проект, и запустите в ней консоль. Затем выполните команду:

```sh
git clone https://github.com/lAkrill/EventsWebApplication.git
```

Перейдите в директорию проекта:

```sh
cd EventsWebApplication/backend/EventsWebApplication
```

Выполните команду:

```sh
docker-compose up --build
```

Если всё успешно прошло, сервер доступен по адресу:

```
http://localhost:8080/swagger/index.html
```

Далее, для большей части функциональности необходимо зарегистрироваться и получить права администратора. Для этого:

1. Зарегистрируйтесь в системе (POST api/user/register в Swagger). 
2. Войдите в систему (POST api/user/login в Swagger). В ответ придёт access и refresh токену, надо скопировать значение accessToken без кавычек. Нажмите значок замка сверху справа на странице swagger с надписью Authorize. Введите туда `Bearer access_token`, без кавычек, вместо access_token вставьте своё значение.
3. Измените роль пользователя на администратора (PUT api/user/{id}/role в Swagger). Используйте id пользователя, полученный при регистрации, значение роли для администратора равно 1.
4. Для выхода из аккайнта в swagger надо снова нажать на значок замка сверху справа страницы с надписью Authorize, и нажать кнопку Logout, а затем повторить действия в пункте 2.

