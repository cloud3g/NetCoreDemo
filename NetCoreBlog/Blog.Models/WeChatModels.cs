using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public enum WC_OfficalAccountType
    {
        /// <summary>
        /// 媒体号
        /// </summary>
        [Display(Name = "媒体号")]
        Media,
        /// <summary>
        /// 企业号
        /// </summary>
        [Display(Name = "企业号")]
        Business,
        /// <summary>
        /// 个人号
        /// </summary>
        [Display(Name = "个人号")]
        personal,
        /// <summary>
        /// 测试号
        /// </summary>
        [Display(Name = "测试号")]
        Text
    }
    public enum WeChatReplyCategory
    {
        //文本
        Text = 1,
        //图文
        Image = 2,
        //语音
        Voice = 3,
        //相等，用于回复关键字
        Equal = 4,
        //包含，用于回复关键字
        Contain = 5
    }

    public enum WeChatRequestRuleEnum
    {
        /// <summary>
        /// 默认回复，没有处理的
        /// </summary>
        Default = 0,
        /// <summary>
        /// 关注回复
        /// </summary>
        Subscriber = 1,
        /// <summary>
        /// 文本回复
        /// </summary>
        Text = 2,
        /// <summary>
        /// 图片回复
        /// </summary>
        Image = 3,
        /// <summary>
        /// 语音回复
        /// </summary>
        Voice = 4,
        /// <summary>
        /// 视频回复
        /// </summary>
        Video = 5,
        /// <summary>
        /// 超链接回复
        /// </summary>
        Link = 6,
        /// <summary>
        /// LBS位置回复
        /// </summary>
        Location = 7,
    }
    public partial class WC_OfficalAccounts : Entity
    {
        /// <summary>
        /// 公众号的唯一ID
        /// </summary>
        [Display(Name = "公众号ID")]
        public string OfficalId { get; set; }
        /// <summary>
        /// 公众号名称
        /// </summary>
        [Display(Name = "公众号名称")]
        public string OfficalName { get; set; }
        /// <summary>
        /// 公众号帐号
        /// </summary>
        [Display(Name = "公众号帐号")]
        public string OfficalCode { get; set; }
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        [Display(Name = "EncodingAESKey")]
        public string OfficalKey { get; set; }
        /// <summary>
        /// 资源服务器
        /// </summary>
        [Display(Name = "资源服务器")]
        public string ApiUrl { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        [Display(Name = "Token")]
        public string Token { get; set; }
        /// <summary>
        /// AppId
        /// </summary>
        [Display(Name = "AppId")]
        public string AppId { get; set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        [Display(Name = "AppSecret")]
        public string AppSecret { get; set; }
        /// <summary>
        /// 访问Token
        /// </summary>
        [Display(Name = "访问Token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        public string Remark { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool Enable { get; set; }
        /// <summary>
        /// 默认
        /// </summary>
        [Display(Name = "默认")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 类别（媒体号，企业号，个人号，开发测试号）
        /// </summary>
        [Display(Name = "类别")]
        public WC_OfficalAccountType Category { get; set; }
        public virtual ICollection<WC_MessageResponse> WC_MessageResponses { get; set; }
    }
    public partial class WC_MessageResponse:Entity
    {
        /// <summary>
        /// 所属公众号
        /// </summary>
        [Display(Name = "所属公众号")]
        public int OfficalAccountId { get; set; }
        /// <summary>
        /// 消息规则
        /// </summary>
        [Display(Name = "消息规则")]
        public WeChatRequestRuleEnum MessageRule { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        public WeChatReplyCategory Category { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        [Display(Name = "关键字")]
        public string MatchKey { get; set; }
        /// <summary>
        /// 文本内容
        /// </summary>
        [Display(Name = "文本内容")]
        public string TextContent { get; set; }
        /// <summary>
        /// 图文文本内容
        /// </summary>
        [Display(Name = "图文文本内容")]
        public string ImgTextContext { get; set; }
        /// <summary>
        /// 图文图片URL
        /// </summary>
        [Display(Name = "图文图片URL")]
        public string ImgTextUrl { get; set; }
        /// <summary>
        /// 图文图片超链接
        /// </summary>
        [Display(Name = "图文图片超链接")]
        public string ImgTextLink { get; set; }
        /// <summary>
        /// 语音URL
        /// </summary>
        [Display(Name = "语音URL")]
        public string MeidaUrl { get; set; }
        /// <summary>
        /// 语音超链接
        /// </summary>
        [Display(Name = "语音超链接")]
        public string MeidaLink { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool Enable { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        [Display(Name = "是否默认")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        public string Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Sort { get; set; }
        [ForeignKey("OfficalAccountId")]
        public virtual WC_OfficalAccounts WC_OfficalAccounts { get; set; }

    } 
}
