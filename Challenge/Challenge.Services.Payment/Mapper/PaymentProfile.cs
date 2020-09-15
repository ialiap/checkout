using System;
using AutoMapper;
using Challenge.Services.Payment.Domain;
using Challenge.Services.Payment.Model.Request;
using Challenge.Services.Payment.Model.Response;

namespace Challenge.Services.Payment.Mapper
{

    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<CreatePaymentBindingModel, Challenge.Services.Payment.Domain.Payment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PaymentStatus.Pending))
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ModificationDateTime, opt => opt.AllowNull());

            CreateMap<Domain.Payment, CreatePaymentResponseModel>();
            CreateMap<Domain.Payment, GetPaymentResponseModel>()
                .ForMember(dest => dest.TransactionDatetime, opt => opt.MapFrom(src=>src.CreationDateTime))
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber.Substring(src.CardNumber.Length - 4).PadLeft(src.CardNumber.Length, '*')));

        }
    }
}