# Taxpayer-Alerter
## Настоятельно рекомендую ознакомиться с описанием
 
## Исходные данные

1.	Excel файл – содержит наименования клиентов, их даты и сумму долга.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/baa5fe11-2bcb-469c-96de-2a65143175e9">
</p>

2.	Word файл – шаблон оферты.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/afa2b84b-ae6b-402f-a507-a462a71b3b72">
</p>

## Пользовательский интерфейс

1. Выбор даты - возможность выбора даты ограничена текущим годом и сегодняшним днем (невозможность выбрать дату, которая еще не наступила и в не в текущем году). По умолчанию стоит сегодняшняя дата.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/0f53130f-a98d-4bb0-b6af-e9b3828fbe3c">
</p>

2.	Для подтверждения выбора даты нужно нажать на кнопку «ОК».

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/d5b0a314-b46b-4043-97af-e15d75b9585d">
</p>

3.	Программа выполняется примерно в течении минуты, так как один post запрос выполняется в среднем 7-8 секунд. При успешном выполнении всех запросов и записи в файлы пользователь увидит всплывающее окно с оповещением, что все готово.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/3199534b-59b5-419d-a309-e635f30ec188">
</p>

Так как программа обращается к удаленному хосту, без интернета программа не будет работать.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/43b64d44-0745-4539-91e1-bcbb86fc370e">
</p>

4.	При успешном выполнении программы, в папке Files исходный Excel файл дополнится, и создадутся файлы оферты в папке Files/ProposalFiles. 

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/c4543c5c-57a1-48ed-8d10-aac44800b2c5">
</p>

Статус «Не присвоен» не был указан в задании, но я добавила его для случаев, если данное наименование не обрабатывается программой, так как не подходит по условию выбранной пользователем даты.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/67ea46d3-5771-4318-a17c-049a7e340368">
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

- Аномалии с наименованием Соседи
Так как я беру первый ответ от сервера, если несколько УНП, то Соседи у меня превратились в ОДО «Энергоресурс» :)
Приложен скриншот из Postman.

<p align="center">
  <img src="https://github.com/nikasuschinskaya/Taxpayer-Alerter/assets/92970744/2774d349-686a-4b60-888b-e351edcfd454">
</p>
