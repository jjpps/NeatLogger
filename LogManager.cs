using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NeatLogger
{
    public class LogManager
    {
        public string LogName { get; private set; }
        public string InitialText { get; private set; } = string.Empty;
        private string DateLog = DateTime.Today.ToString("dd-MM-yyyy");
        private string HourLog = DateTime.Now.ToShortTimeString();
        private string NameLog = $"Log_{DateTime.Today.ToString("dd-MM-yyyy")}.txt";
        public Exception Exception { get; set; }
        public WebException WebException { get; set; }
        public string MetodoName { get; set; }
        public LogManager(Exception exception,WebException wb, string metodoName)
        {
            Exception = exception;
            WebException = wb;
            MetodoName = metodoName;
        }
        public void MontaInitialText()
        {
            if(WebException != null)
            {
                
                InitialText = $"\n\n Erro Mapeado as {HourLog} do dia {DateLog} na função {MetodoName} \n\n -------------------------------\n\n" +
                $" Erro tecnico Codigo Status:{WebException.Status} | {WebException.Message} \n\n -------------------------------\n\n";
            }
            else if(Exception != null)
            {
                InitialText = $"\n\n Erro não reconhecido as {HourLog} do dia {DateLog} na função {MetodoName} \n\n -------------------------------\n\n" +
                $" Erro tecnico Codigo Status:{Exception.Message ?? Exception.InnerException.Message} \n\n -------------------------------\n\n";
            }
            
        }

        public async Task EscreverLog()
        {
            FolderManager folderManager = new FolderManager(@"C:", DateLog,NameLog);
            StringBuilder textoLog =new  StringBuilder();
            textoLog.Clear();
            if (folderManager.CheckExists()) 
            {
                textoLog.Append(CarregaLog(folderManager.MontaPathFile()));
                MontaInitialText();
                textoLog.Append(InitialText);
                using (var sw = new StreamWriter(folderManager.MontaPathFile()))
                {
                    await sw.WriteAsync(textoLog.ToString());
                }
                

            }
        }
        public string CarregaLog(string path)
        {
            string textoLog = string.Empty;
            if (File.Exists(path)) 
            {
                using (StreamReader file = new StreamReader(path))
                {
                    textoLog = file.ReadToEnd();
                }
            }
            
            return textoLog;
        }
      
    }
}
