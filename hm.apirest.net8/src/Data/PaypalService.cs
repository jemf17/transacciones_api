// using PayPal.Core;
// using PayPal.v1.Payments;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// public class PayPalService
// {
//     private readonly PayPalHttpClient _client;

//     public PayPalService()
//     {
//         var environment = new SandboxEnvironment("TU_CLIENT_ID", "TU_SECRET");
//         _client = new PayPalHttpClient(environment);
//     }

//     public async Task<string> CrearPago(decimal monto, string moneda = "USD")
//     {
//         var pago = new Payment()
//         {
//             Intent = "sale",
//             Transactions = new List<Transaction>()
//             {
//                 new Transaction()
//                 {
//                     Amount = new Amount()
//                     {
//                         Total = monto.ToString("F2"),
//                         Currency = moneda
//                     },
//                     Description = "Compra en mi tienda"
//                 }
//             },
//             RedirectUrls = new RedirectUrls()
//             {
//                 ReturnUrl = "https://tu-sitio.com/pago-exitoso",
//                 CancelUrl = "https://tu-sitio.com/pago-cancelado"
//             },
//             Payer = new Payer() { PaymentMethod = "paypal" }
//         };

//         var request = new PaymentCreateRequest();
//         request.RequestBody(pago);
//         var response = await _client.Execute(request);
//         var resultado = response.Result<Payment>();

//         foreach (var link in resultado.Links)
//         {
//             if (link.Rel.Equals("approval_url"))
//             {
//                 return link.Href; // URL de aprobación para redirigir al usuario
//             }
//         }
//         return null;
//     }
// }
