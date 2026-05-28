using AutoMapper;
using DomainLayer.Contracts;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;

        private readonly Lazy<IProductService> _lazyProductService;
        private readonly Lazy<IAuthService> _lazyAuthService;
        private readonly Lazy<IOrderService> _lazyOrderService;
        private readonly Lazy<IPaymentService> _lazyPaymentService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _basketRepository = basketRepository;
            _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _lazyAuthService = new Lazy<IAuthService>(() => new AuthService(_configuration));
            _lazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(_configuration, _basketRepository, _unitOfWork));
            _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(_basketRepository, _unitOfWork, _lazyPaymentService.Value));
        }

        public IProductService ProductService => _lazyProductService.Value;
        public IAuthService AuthService => _lazyAuthService.Value;
        public IOrderService OrderService => _lazyOrderService.Value;
        public IPaymentService PaymentService => _lazyPaymentService.Value;
    }
}
