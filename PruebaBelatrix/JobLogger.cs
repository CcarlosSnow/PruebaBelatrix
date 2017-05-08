using System;
using System.Linq;
using System.Text;

namespace PruebaBelatrix
{
    /*Errores Generales
     * Se están declarando demasiadas variables para el LOG, se debe crear una estructura con los tipos de registro y los tipos de mensajes
     * Se debe crear una clase para poder hacer el registro en la BD, para que separar la parde de BD con la lógica del LOG
     * La cadena de conexión debería estar declarada directamente en una propiedad
     */
    class JobLogger
    {
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        /*Esta variable debería llamarse _logToDatabase para tener una sola forma de declarar todas las variables*/
        private static bool LogToDatabase;
        /*La variable _initialized se declara pero nunca se usa*/
        private bool _initialized;
        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool
        logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            LogToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }
        /*
         La variable bool message de los parámetros se repetía con la variable string message, se cambió el nombre a messageBool
         */
        public static void LogMessage(string message, bool messageBool, bool warning, bool error)
        { 
            message.Trim();
            if (message == null || message.Length == 0)
            {
                /*
                 * Creo que aquí debería ir un throw new Exception("Message can't be null");
                 */
                return;
            }
            if (!_logToConsole && !_logToFile && !LogToDatabase)
            {
                throw new Exception("Invalid configuration");
            }
            if ((!_logError && !_logMessage && !_logWarning) || (!messageBool && !warning && !error))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            /*Se abre la conexión a la BD sin necesidad, la validación inferior debería ir antes de abrir la conexión*/
            connection.Open();
            int t = 0;
            if (messageBool && _logMessage)
            {
                t = 1;
            }
            if (error && _logError)
            {
                t = 2;
            }
            if (warning && _logWarning)
            {
                t = 3;
            }
            /*La inserción a BD debería ir en un if para validar si el LOG va a BD*/
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("Insert into Log Values('" + message + "', " + t.ToString() + ")");
            command.ExecuteNonQuery();
            /*En ningún lado se está cerrando la conexión a la BD*/

            /*Todo el registro del archivo de texto debería ir dentro de un if para poder validar el log se registrará en un Archivo*/
            string l = "";
            if (!System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))
            {
                /*Aquí la aplicación se caería ya que está preguntando si no existe, si es así leer el archivo, no se puede leer un archivo inexistente, el if debería ser si es archivo existe*/
                l = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt");
            }/*Aquí debería haber un else por si el archivo no existe*/

            /*Se repiten 3 if por gusto, ya que todos hacen lo mismo*/
            if (error && _logError)
            {
                /*El DateTime.Now.ToShortDateString() debería ser solo DateTime.Now para que aparezca las horas, minutos y segundos donde ocurrió LOG */
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            if (warning && _logWarning)
            {
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            if (messageBool && _logMessage)
            {
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            /*Esta línea debería ir en el *else* donde se pregunta si el archivo existe*/
            System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", l);
            if (error && _logError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (warning && _logWarning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (messageBool && _logMessage)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            /*Esta línea debería estar dentro de un if para validar si el LOG va a consola*/
            Console.WriteLine(DateTime.Now.ToShortDateString() + message);
        }
    }
}