
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;

namespace Web.Business
{
    public class ItemBLL : BaseBLL
    {
        private ItemDAL itemDAL;
        private ItemViewDAL viewDAL;
        private ItemCommentDAL commentDAL;
        private ItemImageDAL imageDAL;
        private ItemVoteDAL voteDAL;
        private ItemLikeDAL likeDAL;

        public ItemBLL(string connectionString = "")
            : base(connectionString)
        {
            itemDAL = new ItemDAL(this.DatabaseFactory);
            viewDAL = new ItemViewDAL(this.DatabaseFactory);
            commentDAL = new ItemCommentDAL(this.DatabaseFactory);
            imageDAL = new ItemImageDAL(this.DatabaseFactory);
            voteDAL = new ItemVoteDAL(this.DatabaseFactory);
            likeDAL = new ItemLikeDAL(this.DatabaseFactory);
        }

        #region Item
        public void ChangeOrder(int id, int companyId, int order)
        {
            var entity = this.itemDAL.GetAll().FirstOrDefault(e => e.Id == id && e.CompanyId == companyId);
            if (entity != null)
            {
                entity.Orders = order;
                this.itemDAL.Update(entity);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }

        public void ChangePublish(int id, int companyId)
        {
            var entity = this.itemDAL.GetAll().FirstOrDefault(e => e.Id == id && e.CompanyId == companyId);
            if (entity != null)
            {
                entity.IsPublished = !entity.IsPublished;
                this.itemDAL.Update(entity);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }

        public void Delete(int id, int companyId)
        {
            var entity = this.itemDAL.GetAll().FirstOrDefault(e => e.Id == id && e.CompanyId == companyId);
            if (entity != null)
            {
                this.itemDAL.Delete(entity);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }
        #endregion

        #region View
        public void UpView(int id, int companyId)
        {
            var view = viewDAL.GetAll().FirstOrDefault(e => e.ItemId == id && e.Item.CompanyId == companyId);
            if (view == null)
            {
                var entity = this.itemDAL.GetAll().FirstOrDefault(e => e.Id == id && e.CompanyId == companyId);
                if (entity != null)
                {
                    view = new ItemView();
                    view.Item = entity;
                    view.Views = 1;
                    viewDAL.Add(view);
                }
                else { throw new Exception("Data does not exist"); }
            }
            else
            {
                view.Views += 1;
                viewDAL.Update(view);
            }

            this.SaveChanges();
        }
        #endregion

        #region Comment
        public int CountNewComment(int companyId, int alert)
        {
            var before = DateTime.Now.AddDays(-alert).Date;
            var comments = this.commentDAL.GetAll().Count(e => e.Item.CompanyId == companyId && e.Item.ModifyDate > before);
            return comments;
        }

        public int CreateComment(ITEMCOMMENTModel model, int companyId)
        {
            var entity = this.itemDAL.GetAll().FirstOrDefault(e => e.Id == model.ITEMID && e.CompanyId == companyId);
            if (entity == null) throw new Exception("Data does not exist");

            var comment = new ItemComment();
            comment.ClientId = model.CLIENTID;
            comment.Description = model.CONTENT;
            comment.Email = model.EMAIL;
            comment.Phone = model.PHONE;
            comment.Name = model.NAME;
            comment.ItemId = model.ITEMID;
            comment.Item = new Item();
            comment.Item.CompanyId = companyId;
            comment.Item.ModifyDate = DateTime.Now;
            comment.Item.ModifyByUser = entity.ModifyByUser;
            comment.Item.IsPublished = true;
            this.commentDAL.Add(comment);
            this.SaveChanges();
            return comment.Id;
        }

        public IQueryable<ITEMCOMMENTModel> GetComments(int id, int companyId)
        {
            var allIds = commentDAL.GetAll().Where(e => e.Item.CompanyId == companyId)
                            .Select(e => new CommentId
                            {
                                ID = e.Id,
                                ItemId = e.ItemId,
                            }).ToList();

            var listId = allIds.Select(e => e.ID).ToList();
            if (id > 0) listId = GetAllChildId(allIds, id);

            var comments = this.commentDAL.GetAll().Where(e => listId.Contains(e.Id))
                            .Select(e => new ITEMCOMMENTModel
                            {
                                ID = e.Id,
                                NAME = e.Name,
                                CLIENTID = e.ClientId,
                                CONTENT = e.Description,
                                EMAIL = e.Email,
                                ITEMID = e.ItemId,
                                PHONE = e.Phone,
                                Date = e.Item.ModifyDate,
                                UserId = e.Item.ModifyByUser,
                                Like = e.Item.ItemLike == null ? 0 : e.Item.ItemLike.Likes,
                                DisLike = e.Item.ItemLike == null ? 0 : e.Item.ItemLike.UnLikes
                            }).OrderByDescending(e => e.Date);

            return comments;
        }

        private List<int> GetAllChildId(List<CommentId> items, int? itemId)
        {
            var subcats = items.Where(o => o.ItemId == itemId).ToList(); // lay tat ca con cua parentID
            if (!subcats.Any()) return new List<int>();
            var result = new List<int>();
            foreach (var subcat in subcats)
            {
                if (subcat != null)
                {
                    var temp = this.GetAllChildId(items, subcat.ID);

                    result.Add(subcat.ID);
                    if (temp != null && temp.Count > 0)
                    {
                        result.AddRange(temp);
                    }
                }
            }

            return result;
        }

        class CommentId
        {
            public int ID { get; set; }
            public int ItemId { get; set; }
        }
        #endregion

        #region Image
        public IQueryable<ItemImageModel> GetImages(int id, int companyId)
        {
            var images = this.imageDAL.GetAll().Where(e => e.ItemId == id && e.Item.CompanyId == companyId)
                            .Select(e => new ItemImageModel
                            {
                                ID = e.Id,
                                Image = e.ImageName,
                                ItemId = e.ItemId
                            });

            return images;
        }
        #endregion

        #region Like
        public void UpdateLike(int id, int companyId, bool like = true)
        {
            var item = this.likeDAL.Get(e => e.ItemId == id);
            if (item == null)
            {
                item = new ItemLike();
                item.ItemId = id;
                item.Likes = 0;
                item.UnLikes = 0;

                this.likeDAL.Add(item);
            }
            else
            {
                if(like) item.Likes += 1;
                else item.UnLikes += 1;

                this.likeDAL.Update(item);
            }

            try
            {
                this.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
        public ITEMLIKEModel GetLikes(int id, int companyId)
        {
            var item = this.likeDAL.GetAll().Where(e => e.ItemId == id)
                            .Select(e => new ITEMLIKEModel()
                            {
                                ID = e.ItemId,
                                LIKES = e.Likes,
                                UNLIKES = e.UnLikes
                            })
                            .FirstOrDefault() ;
            return item;
        }
        #endregion

        #region Vote
        public ItemVoteModel UpdateVote(ItemVoteModel model)
        {
            var item = this.voteDAL.Get(e => e.ItemId == model.Id);
            if (item == null)
            {
                item = new ItemVote();
                item.ItemId = model.Id;
                item.VoteNumber = 1;
                item.VoteRate = model.VoteRate;

                this.voteDAL.Add(item);
            }
            else
            {
                item.VoteNumber += 1;
                item.VoteRate += model.VoteRate;

                this.voteDAL.Update(item);
            }

            try
            {
                this.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

            model.VoteNumber = item.VoteNumber;
            model.VoteRate = item.VoteRate;
            return model;
        }
        #endregion
    }
}
