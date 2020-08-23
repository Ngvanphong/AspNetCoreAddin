using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.EF;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCoreAddin.Application.Implementations
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Comment, int> _commentRepository;
        private IMapper _mapper;
        public CommentService(IUnitOfWork unitOfWork, IRepository<Comment,int> commentRepository,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public void Add(CommentViewModel commentVm)
        {
            _commentRepository.Add(_mapper.Map<Comment>(commentVm));
        }

        public void Delete(int id)
        {
            _commentRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<CommentViewModel> GetCommentByTagPagging(string tag, int page, int pageSize, out int totalRow)
        {
            var listComment = _commentRepository.FindAll();
            totalRow = listComment.Count();
            listComment = listComment.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<CommentViewModel>>(listComment.ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
