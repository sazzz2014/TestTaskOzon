# TestTaskOzon

Для запуска проекта нужно:
1. В файле appsettings.json нужно указать подключение к базе данных(в переменную DefaultConnection)
2. Добавить миграции(https://metanit.com/sharp/entityframework/3.12.php).
   Для этого в консоли диспетчере пакетов nuget нужно указать две команды:
	a)add-migration "Свое название миграции"
	б)update-database

Пример get запроса: https://localhost:44336/IpAddress?from=255.255.255.234&to=255.255.255.255
	 