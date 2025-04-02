using AutoMapper;
using BusinessLogic.DTO.FrameDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class FrameService : IFrameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FrameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FrameResponseDTO> CreateFrame(FrameRequestDTO frameRequestDTO)
        {
            var frame = _mapper.Map<Frame>(frameRequestDTO);
            await _unitOfWork.Frame.AddAsync(frame);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<FrameResponseDTO>(frame);
        }

        public async Task<bool> DeleteFrame(int id)
        {
            var frame = await _unitOfWork.Frame.GetFirstOrDefaultAsync(f => f.Id == id);
            if (frame == null)
            {
                return false;
            }
            _unitOfWork.Frame.Remove(frame);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<FrameResponseDTO> GetFrame(int id)
        {
            var frames = await _unitOfWork.Frame.GetFirstOrDefaultAsync(f => f.Id == id, includeProperties: "FrameStyle,Coordinates");
            if (frames == null)
            {
                return null;
            }
            frames.Coordinates = frames.Coordinates
                .Where(sticker => !sticker.IsDeleted)
                .ToList();

            return _mapper.Map<FrameResponseDTO>(frames);
        }

        public async Task<IEnumerable<FrameResponseDTO>> GetFrameByFrameStyleId(int frameStyleId)
        {
            var frames = await _unitOfWork.Frame.GetAllAsync(f => f.FrameStyleId == frameStyleId, includeProperties: "FrameStyle,Coordinates");
            foreach (var frame in frames)
            {
                frame.Coordinates = frame.Coordinates
                    .Where(sticker => !sticker.IsDeleted)
                    .ToList();
            }
            return _mapper.Map<IEnumerable<FrameResponseDTO>>(frames);
        }

        public async Task<IEnumerable<FrameResponseDTO>> GetFrames()
        {
            var frames = await _unitOfWork.Frame.GetAllAsync(includeProperties: "FrameStyle,Coordinates");
            foreach (var frame in frames)
            {
                frame.Coordinates = frame.Coordinates
                    .Where(sticker => !sticker.IsDeleted)
                    .ToList();
            }
            return _mapper.Map<IEnumerable<FrameResponseDTO>>(frames);
        }

        public async Task<IEnumerable<FrameResponseDTO>> SearchFrame(string frameName)
        {
            var frames = await _unitOfWork.Frame.GetAllAsync(f => f.Name.ToLower().Contains(frameName.ToLower()), includeProperties: "FrameStyle,Coordinates");
            foreach (var frame in frames)
            {
                frame.Coordinates = frame.Coordinates
                    .Where(sticker => !sticker.IsDeleted)
                    .ToList();
            }
            return _mapper.Map<IEnumerable<FrameResponseDTO>>(frames);
        }

        public async Task<FrameResponseDTO> UpdateFrame(int id, FrameRequestDTO frameRequestDTO)
        {
            var frame = await _unitOfWork.Frame.GetFirstOrDefaultAsync(f => f.Id == id);
            if(!string.IsNullOrEmpty(frameRequestDTO.Name))
            {
                frame.Name = frameRequestDTO.Name;
            }
            if (!string.IsNullOrEmpty(frameRequestDTO.FrameUrl))
            {
                frame.FrameUrl = frameRequestDTO.FrameUrl;
            }
            if (frameRequestDTO.FrameStyleId.HasValue)
            {
                frame.FrameStyleId = frameRequestDTO.FrameStyleId.Value;
            }
            if (frameRequestDTO.SlotCount.HasValue)
            {
                frame.SlotCount = frameRequestDTO.SlotCount.Value;
            }
            await _unitOfWork.SaveAsync();
            return _mapper.Map<FrameResponseDTO>(frame);
        }
    }
}
