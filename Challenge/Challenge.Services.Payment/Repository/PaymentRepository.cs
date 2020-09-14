using AutoMapper;
using Raven.Client.Documents;

namespace Challenge.Services.Payment.Repository
{
    public class PaymentRepository : RavenRepository<Domain.Payment>
    {
        public PaymentRepository(IDocumentStore store, IMapper mapper) : base(store, mapper)
        {
        }

    }
}