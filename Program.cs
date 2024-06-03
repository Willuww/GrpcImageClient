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
            using var channel = GrpcChannel.ForAddress("http://localhost:5276"); // Dirección del servidor gRPC
            var client = new AutorImagenService.AutorImagenServiceClient(channel); // Crea el cliente gRPC

            // Ruta de la imagen
            var imagePath = @"C:\Users\joelb\Desktop\IMG-20181102-WA0003.jpg"; // Asegúrate de que este camino sea correcto

            // Lee la imagen del archivo
            byte[] imageBytes = await File.ReadAllBytesAsync(imagePath); // Lee el archivo y convierte a un array de bytes

            // Crea la solicitud de imagen
            var request = new ImagenRequest { Contenido = Google.Protobuf.ByteString.CopyFrom(imageBytes) }; // Crea la solicitud con el contenido de la imagen

            // Envía la solicitud y recibe la respuesta
            var response = await client.GuardarImagenAsync(request); // Envía la solicitud al servidor

            // Imprime el resultado
            Console.WriteLine(response.Mensaje); // Muestra la respuesta en la consola
        }
    }
}
