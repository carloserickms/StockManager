using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;

namespace StockManager.Seeds
{
    public static class ColorTableSeed
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Color.Any())
            {
                var colorsList = new List<Color>
                {
                    new Color("Vermelho"),
                    new Color("Verde"),
                    new Color("Azul"),
                };

                context.Color.AddRange(colorsList);
                context.SaveChanges();
            }
        }
    }

    public static class PaymentMethodSeed
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.PaymentMethod.Any())
            {
                var paymentMethodList = new List<PaymentMethod>
                {
                    new PaymentMethod("Cartão de credito"),
                    new PaymentMethod("Cartão de Débito"),
                    new PaymentMethod("Dinheiro"),
                    new PaymentMethod("Pix"),
                };

                context.PaymentMethod.AddRange(paymentMethodList);
                context.SaveChanges();
            }
        }
    }

    public static class StatusSeed
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Status.Any())
            {
                var StatusList = new List<Status>
                {
                    new Status("Entregue"),
                    new Status("Aguardando Entrega"),
                    new Status("Cancelado"),
                };

                context.Status.AddRange(StatusList);
                context.SaveChanges();
            }
        }
    }
}