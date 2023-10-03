using Aspose.Words;
using System.Configuration;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.Writers.Base;

namespace TaxpayerAlerter.DAL.Writers
{
    public class DOCXWriter : IWriter<ClientDAO>
    {
        private readonly string _docPath = ConfigurationManager.AppSettings["docPath"].ToString();

        public async Task Write(List<ClientDAO> clients)
        {
            var builder = new DocumentBuilder();

            var font = builder.Font;
            font.Size = 11;
            font.Color = System.Drawing.Color.Black;
            font.Name = "Calibri";

            foreach (var client in clients)
            {
                var doc = new Document();

                builder.Document = doc;

                builder.Writeln($"\nУважаемые: {client.FullName}, информируем вас о том, что вы не погасили кредит\n");
                builder.Writeln("Ваша задолженность: \n");

                builder.StartTable();

                PutTableCell(builder, "Наименование");
                PutTableCell(builder, "УНП");
                PutTableCell(builder, "Сумма");
                builder.EndRow();

                PutTableCell(builder, client.FullName);
                PutTableCell(builder, client.Unp);
                PutTableCell(builder, client.Sum.ToString());
                builder.EndRow();

                builder.EndTable();

                builder.Writeln("\nПлатите и фигней не занимайтесь :) \n");

                await Task.Run(() => doc.Save(_docPath + $"{client.Unp}-{DateTime.Now.ToShortDateString()}.docx"));
            }
        }

        private void PutTableCell(DocumentBuilder builder, string textCell)
        {
            builder.InsertCell();
            builder.Write(textCell);
        }
    }
}