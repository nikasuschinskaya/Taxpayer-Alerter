using Aspose.Words;
using System.Configuration;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.WriteWorkers.Base;

namespace TaxpayerAlerter.DAL.WriteWorkers
{
    public class DOCXWriteWorker : IWriteWorker
    {
        private string _docPath = ConfigurationManager.AppSettings["docPath"].ToString();

        public void Write(Client client)
        {
            Document doc = new();
            DocumentBuilder builder = new(doc);

            Font font = builder.Font;
            font.Size = 11;
            font.Color = System.Drawing.Color.Black;
            font.Name = "Calibri";

            builder.Writeln($"\nУважаемые: {client.FullName}, информируем вас о том, что вы не погасили кредит\n");
            builder.Writeln("Ваша задолженность: \n");

            builder.StartTable();

            PutTableCell(builder, "Наименование");
            PutTableCell(builder, "УНП");
            PutTableCell(builder, "Сумма");
            builder.EndRow();

            PutTableCell(builder, client.FullName.ToString());
            PutTableCell(builder, client.Unp.ToString());
            PutTableCell(builder, client.Sum.ToString());
            builder.EndRow();

            builder.EndTable();

            builder.Writeln("\nПлатите и фигней не занимайтесь :) \n");

            doc.Save(_docPath + $"{client.Unp}-{DateTime.Now.ToShortDateString()}.docx");
        }

        private void PutTableCell(DocumentBuilder builder, string textCell)
        {
            builder.InsertCell();
            builder.Write(textCell);
        }
    }
}