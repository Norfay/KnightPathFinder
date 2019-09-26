using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ChessApp1
{
    class Program
    {        
        static void Main(string[] args)
        {

            HttpListener listener = new HttpListener();

            listener.Prefixes.Add("http://localhost:8888/findpath/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");

            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;


            var body = GetRequestPostData(request);
            var data = JsonConvert.DeserializeObject<CoordinatesData>(body);

            PathFinder pathFinder = new PathFinder();

            var start = data.From;
            var finish = data.To;

            if (start == finish)
            {               
                Console.WriteLine("Начальная и конечная клетки совпадают");
                Console.ReadKey();
                throw new Exception("Начальная и конечная клетки совпадают");
            }

            if (!pathFinder.CoordinateValidation(start))
            {
                Console.WriteLine("Задана неверная начальная клетка поля");
                Console.ReadKey();
                throw new Exception("Задана неверная начальная клетка поля");                
            }

            if (!pathFinder.CoordinateValidation(finish))
            {
                Console.WriteLine("Задана неверная конечная клетка поля");
                Console.ReadKey();
                throw new Exception("Задана неверная конечная клетка поля");
            }

            var startXMatrix = pathFinder.ChessToMatrixCoordinate(start);
            var finishYMatrix = pathFinder.ChessToMatrixCoordinate(finish);            

            List<string> path = new List<string>();

            var matrix = pathFinder.CreateMatrix();

            var stepMap = pathFinder.Bfs(startXMatrix, finishYMatrix, matrix);

            var rawShortestPath = pathFinder.RestorePath(startXMatrix, finishYMatrix, stepMap, matrix);

            foreach(var step in rawShortestPath)
            {
                path.Add(pathFinder.MatrixToChessCoordinate(step));
            }

            HttpListenerResponse response = context.Response;

            string responseStr = JsonConvert.SerializeObject(path);
            byte[] buffer = Encoding.UTF8.GetBytes(responseStr);

            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
                        
            Console.WriteLine("Обработка подключений завершена");            
            Console.ReadKey();

            output.Close();

            listener.Stop();
        }

        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (Stream body = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }        
    }
}
