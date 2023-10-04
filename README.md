# Taxpayer-Alerter
<h2>Настоятельно рекомендую ознакомиться с описанием</h2>
 
## Исходные данные

- Excel файл – содержит наименования клиентов, их даты и сумму долга.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/943cf314-29c7-422e-a849-bc77e74bd533">
</p>

- Word файл – шаблон оферты.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/afa2b84b-ae6b-402f-a507-a462a71b3b72">
</p>

## Пользовательский интерфейс

<h3>Для примера рассматривается использование программы за 03.10.2023</h3>

- Выбор даты - возможность выбора даты ограничена текущим годом и сегодняшним днем (невозможность выбрать дату, которая еще не наступила и в не в текущем году). По умолчанию стоит сегодняшняя дата.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/0486dd39-6870-4700-af87-790f30d14c3d">
</p>

- Для подтверждения выбора даты нужно нажать на кнопку «ОК».

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/e73ab24d-2b7b-4366-96b3-58ca30f49293">
</p>

- Программа выполняется около минуты (может чуть больше), так как один post запрос выполняется в среднем 7-8 секунд (при хорошей скорости интернета). При успешном выполнении всех запросов и записи в файлы пользователь увидит всплывающее окно с оповещением, что все готово.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/f711e19a-a27d-4bff-a289-0ad5b0d14bd5">
</p>

Так как программа обращается к удаленному серверу, без интернета программа не будет работать.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/43b64d44-0745-4539-91e1-bcbb86fc370e">
</p>

- При успешном выполнении программы, в папке Files исходный Excel файл дополнится, и создадутся файлы оферты в папке Files/ProposalFiles. 

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/c4543c5c-57a1-48ed-8d10-aac44800b2c5">
</p>

Статус «Не присвоен» не был указан в задании, но я добавила его для случаев, если данное наименование не обрабатывается программой, так как не подходит по условию выбранной пользователем даты.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/3f50dd6f-13ae-4d64-95ad-32164d25f862">
</p>

Файл 100921458-03.10.2023.docx

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/f46bf555-4c07-4236-abef-8c93310f0285">
</p>

## Дополнительная информация

- WPF приложение с использованием паттерна MVVM
- DI-контейнер Autofac
- Трехуровневая архитектура 
- Логгирование в файл с использованием библиотеки Serilog
- Обработка файлов с расширением .docx/.doc с использованием библиотеки Aspose (бесплатный пакет, поэтому в файлах будет содержатся водяной знак и красные надписи от библиотеки)
- Обработка файлов с расширением .xlsx/.xls с использованием библиотеки IronXL

## Любопытная информация

- Аномалии с наименованием Соседи (остальные наименования проходят нормально)

Так как я беру первый ответ от сервера, если несколько УНП, то Соседи у меня превратились в ОДО «Энергоресурс» :)

Приложен скриншот из Postman.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/2774d349-686a-4b60-888b-e351edcfd454">
</p>
