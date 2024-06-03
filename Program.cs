using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcImagenService;

namespace GrpcImageClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Configura la dirección del servidor gRPC
            using var channel = GrpcChannel.ForAddress("http://localhost:5276");
            var client = new AutorImagenService.AutorImagenServiceClient(channel);

            // Especifica la ruta de la imagen
            var imagePath = @"C:\Users\joelb\Desktop\IMG-20181102-WA0003.jpg"; 
            
            // Verifica si el archivo existe
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("El archivo no existe: " + imagePath);
                return;
            }

            // Lee la imagen del archivo
            byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);

            // Crea la solicitud de imagen
            var request = new ImagenRequest { Contenido = Google.Protobuf.ByteString.CopyFrom(imageBytes) };

            // Envía la solicitud y recibe la respuesta
            var response = await client.GuardarImagenAsync(request);

            // Imprime el resultado
            Console.WriteLine(response.Mensaje);
        }
    }
}
