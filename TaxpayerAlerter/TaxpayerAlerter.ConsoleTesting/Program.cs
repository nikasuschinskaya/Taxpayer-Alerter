using Aspose.Words.Tables;
using Aspose.Words;
using IronXL;
using System.Configuration;
using System.Diagnostics.Metrics;
using TaxpayerAlerter.DAL.ModelsDAO;
using Range = IronXL.Range;

string _doc = "C:/Study/Alfa Bank/Test task/";

List<Client> clients = new()
{
    new Client(){ Unp = 7834, FullName = "MIMICHI", Sum = 674},
    new Client(){ Unp = 5435, FullName = "SUCHI", Sum = 100}
};

DocumentBuilder builder = new DocumentBuilder();

Font font = builder.Font;
font.Size = 11;
font.Color = System.Drawing.Color.Black;
font.Name = "Calibri";

foreach (Client client in clients)
{
    Document doc = new Document();

    builder.Document = doc;

    builder.Writeln($"\nУважаемые: {client.FullName}, информируем вас о том, что вы не погасили кредит\n");
    builder.Writeln("Ваша задолженность: \n");

    builder.StartTable();

    builder.InsertCell();

    builder.Write("Наименование");

    builder.InsertCell();
    builder.Write("УНП");

    builder.InsertCell();
    builder.Write("Сумма");
    builder.EndRow();

    builder.InsertCell();
    builder.Write(client.FullName.ToString());

    builder.InsertCell();
    builder.Write(client.Unp.ToString());

    builder.InsertCell();
    builder.Write(client.Sum.ToString());
    builder.EndRow();

    builder.EndTable();

    builder.Writeln("\nПлатите и фигней не занимайтесь :) \n");

    doc.Save(_doc + $"{client.Unp}-{DateTime.Now.ToShortDateString()}.docx");
}



//    Document doc = new();
//DocumentBuilder builder = new(doc);

//Font font = builder.Font;
//font.Size = 11;
//font.Color = System.Drawing.Color.Black;
//font.Name = "Calibri";


//builder.Writeln($"\nУважаемые: {client.FullName}, информируем вас о том, что вы не погасили кредит\n");
//builder.Writeln("Ваша задолженность: \n");

//Table table = builder.StartTable();

//builder.InsertCell();

//builder.Write("Наименование");

//builder.InsertCell();
//builder.Write("УНП");

//builder.InsertCell();
//builder.Write("Сумма");
//builder.EndRow();

//builder.InsertCell();
//builder.Write(client.FullName.ToString());

//builder.InsertCell();
//builder.Write(client.Unp.ToString());

//builder.InsertCell();
//builder.Write(client.Sum.ToString());
//builder.EndRow();

//builder.EndTable();
//builder.Writeln();

//builder.Writeln("\nПлатите и фигней не занимайтесь :) \n");

//doc.Save(_doc + $"{client.Unp}-{DateTime.Now.ToShortDateString()}.docx");


//string _xlsxPath = "C:/Users/User/source/repos/Taxpayer-Alerter/TaxpayerAlerter/TaxpayerAlerter.DAL/Files/Для тестового.xlsx";

//WorkBook workBook = WorkBook.Load(_xlsxPath);
//WorkSheet workSheet = workBook.WorkSheets.First();

//List<Client> clients = new List<Client>();


//for (var i = 2; i <= 10; i++)
//{
//    var range = workSheet[$"A{i}:D{i}"].ToList();
//    var client = new Client
//    {
//        Name = range[0].Value.ToString(),
//        Date = DateTime.Parse(range[2].Value.ToString()),
//        Sum = int.Parse(range[3].Value.ToString())
//    };
//    clients.Add(client);
//}

//foreach (var item in clients)
//{
//    Console.WriteLine(item.Name);
//    Console.WriteLine(item.Date.ToString());
//    Console.WriteLine(item.Sum.ToString());
//}



//Range range = workSheet["A2:A10"];

//for (int i = 0; i < 9; i++)
//{
//    var names = workSheet["A2:A10"].Select(name => name.Text).ToArray();
//    var dates = workSheet["C2:C10"].Select(date => DateTime.Parse(date.Text)).ToArray();
//    var sums = workSheet["D2:D10"].Select(sum => int.Parse(sum.Text)).ToArray();
//    client.Name = names[i];   
//    client.Date = dates[i];   
//    client.Sum = sums[i];   

//    clients.Add(client);
//    i++;
//}

//var names = workSheet["A2:A10"].Select(name => name.Text).ToArray();
//var dates = workSheet["C2:C10"].Select(date => DateTime.Parse(date.Text)).ToArray();
//var sums = workSheet["D2:D10"].Select(sum => int.Parse(sum.Text)).ToArray();

//foreach (var item in names)
//{
//    Console.WriteLine(item);
//}
//foreach (var item in dates)
//{
//    Console.WriteLine(item);
//}
//foreach (var item in sums)
//{
//    Console.WriteLine(item);
//}



//    foreach (var cell in workSheet["A2:A10"])
//    client.Name = cell.Text;
////clients.Add(new Client { Name = cell.Text });

//foreach (var cell in workSheet["C2:C10"])
//    client.Date = DateTime.Parse(cell.Text);
////clients.Add(new Client { Date = DateTime.Parse(cell.Text) });

//foreach (var cell in workSheet["D2:D10"])
//    client.Sum = int.Parse(cell.Text);
//    //clients.Add(new Client { Sum = int.Parse(cell.Text) });


//foreach (var item in clients)
//{
//    Console.WriteLine(item.Name);
//    Console.WriteLine(item.Date.ToString());
//    Console.WriteLine(item.Sum.ToString());
//}
