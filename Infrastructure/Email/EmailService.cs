using MailKit.Security;
using Microsoft.Extensions.Configuration;

using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using Microsoft.Extensions.Logging;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;

namespace TiendaUNAC.Infrastructure.Email
{
    public interface IEmailService
    {
        Task<bool> EnviarEmailCodigo(EmailEnviar emailEnviar);
        Task<bool> EnviarEmailConfirmacionPedido(RegistrarPedido pedido, string correoDestinatario, string asunto, int orden);
    }

    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        #region CODIGO
        public async Task<bool> EnviarEmailCodigo(EmailEnviar emailEnviar)
        {
            _logger.LogTrace("Iniciando metodo EmailService.EnviarEmailCodigo...");
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:UserName").Value));
                email.To.Add(MailboxAddress.Parse(emailEnviar.Para));
                email.Subject = emailEnviar.Asunto;
                string contenido = emailEnviar.Contenido.Replace("[Nombre]", emailEnviar.Nombre);

                contenido = contenido.Replace("[Código de seguridad]", emailEnviar.Codigo);

                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = contenido
                };

                using var smtp = new SmtpClient();
                smtp.Connect(
                    _configuration.GetSection("Email:Host").Value,
                    Convert.ToInt32(_configuration.GetSection("Email:Port").Value),
                SecureSocketOptions.StartTls
                );
                smtp.Authenticate(_configuration.GetSection("Email:UserName").Value, _configuration.GetSection("Email:PassWord").Value);

                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo EmailService.EnviarEmailCodigo...");
                throw;
            }
        }
        #endregion

        #region CONFIRMACION PEDIDO
        public async Task<bool> EnviarEmailConfirmacionPedido(RegistrarPedido pedido, string correoDestinatario, string asunto, int orden)
        {
            _logger.LogTrace("Iniciando metodo EmailService.EnviarEmailConfirmacionPedido...");
            try
            {
                string contenido = asunto;

                contenido = contenido.Replace("[Número de pedido]", orden.ToString())
                                     .Replace("[SubTotal]", pedido.SubTotal.ToString("N2"))
                                     .Replace("[ValorEnvio]", pedido.ValorEnvio.ToString("N2"))
                                     .Replace("[ValorDescuento]", pedido.ValorDescuento.ToString("N2"))
                                     .Replace("[ValorTotal]", pedido.ValorTotal.ToString("N2"));

                
                var registrosHtml = new StringBuilder();
                foreach (var registro in pedido.Registros)
                {
                    registrosHtml.Append("<tr>")
                                 .Append($"<td><img src='https://apitienda.unac.edu.co/{registro.imagen}' alt='' /> {registro.Nombre}</td>")
                                 .Append($"<td>{registro.Color}</td>")
                                 .Append($"<td>{(string.IsNullOrEmpty(registro.Talla) ? "No Aplica" : registro.Talla)}</td>")
                                 .Append($"<td>{registro.Cantidad}</td>")
                                 .Append($"<td>${registro.ValorUnidad.ToString("N2")}</td>")
                                 .Append($"<td>${(registro.ValorUnidad * registro.Cantidad).ToString("N2")}</td>")
                                 .Append("</tr>");
                }

                contenido = contenido.Replace("<!-- Aquí irán las filas de productos -->", registrosHtml.ToString());

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:UserName").Value));
                email.To.Add(MailboxAddress.Parse(correoDestinatario));
                email.Subject = "Confirmación de tu pedido";

                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = contenido
                };

                using var smtp = new SmtpClient();
                smtp.Connect(
                    _configuration.GetSection("Email:Host").Value,
                    Convert.ToInt32(_configuration.GetSection("Email:Port").Value),
                    SecureSocketOptions.StartTls
                );
                smtp.Authenticate(_configuration.GetSection("Email:UserName").Value, _configuration.GetSection("Email:PassWord").Value);

                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo EmailService.EnviarEmailConfirmacionPedido...");
                throw;
            }
        }
        #endregion

    }
}
