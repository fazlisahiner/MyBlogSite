using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using MimeKit;
using System.Net.Mail;




namespace BlogSite.Web.Models
{
    public static class HtmlHelperTo
    {
        public static string TruncateHtml(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input) || maxLength <= 0)
                return string.Empty;

            var doc = new HtmlDocument();
            doc.LoadHtml(input);

            if (doc.DocumentNode == null)
                return string.Empty;

            var currentLength = 0;
            var truncatedHtml = TruncateNode(doc.DocumentNode, ref currentLength, maxLength);

            return truncatedHtml;
        }

        private static string TruncateNode(HtmlNode node, ref int currentLength, int maxLength)
        {
            if (node.NodeType == HtmlNodeType.Text)
            {
                var textLength = node.InnerText.Length;
                if (currentLength + textLength > maxLength)
                {
                    var remainingLength = maxLength - currentLength;
                    var truncatedText = node.InnerText.Substring(0, remainingLength);
                    currentLength += remainingLength;
                    return HtmlEntity.Entitize(truncatedText);
                }
                else
                {
                    currentLength += textLength;
                    return node.OuterHtml;
                }
            }
            else
            {
                var truncatedInnerHtml = new List<string>();
                foreach (var childNode in node.ChildNodes)
                {
                    if (currentLength >= maxLength)
                        break;
                    truncatedInnerHtml.Add(TruncateNode(childNode, ref currentLength, maxLength));
                }
                var outerHtml = node.CloneNode(false).OuterHtml;
                var closingTagIndex = outerHtml.IndexOf(">");
                return outerHtml.Insert(closingTagIndex + 1, string.Join("", truncatedInnerHtml));
            }
        }




        //public static string TruncateHtml(string input, int maxLength)
        //{
        //    if (string.IsNullOrEmpty(input) || maxLength <= 0)
        //        return string.Empty;

        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(input);

        //    if (doc.DocumentNode == null)
        //        return string.Empty;

        //    var currentLength = 0;
        //    var truncatedHtmlNodes = new List<HtmlNode>();
        //    TruncateNode(doc.DocumentNode, truncatedHtmlNodes, ref currentLength, maxLength);

        //    var truncatedDoc = new HtmlDocument();
        //    foreach (var node in truncatedHtmlNodes)
        //    {
        //        truncatedDoc.DocumentNode.AppendChild(node.Clone());
        //    }

        //    return truncatedDoc.DocumentNode.InnerHtml;
        //}

        //private static bool TruncateNode(HtmlNode node, List<HtmlNode> truncatedNodes, ref int currentLength, int maxLength)
        //{
        //    if (node.NodeType == HtmlNodeType.Text)
        //    {
        //        var textLength = node.InnerText.Length;
        //        if (currentLength + textLength > maxLength)
        //        {
        //            var remainingLength = maxLength - currentLength;
        //            var truncatedText = node.InnerText.Substring(0, remainingLength);
        //            var textNode = HtmlNode.CreateNode(truncatedText);
        //            truncatedNodes.Add(textNode);
        //            currentLength += remainingLength;
        //            return false;
        //        }
        //        else
        //        {
        //            truncatedNodes.Add(node);
        //            currentLength += textLength;
        //        }
        //    }
        //    else
        //    {
        //        var clonedNode = node.CloneNode(false);
        //        truncatedNodes.Add(clonedNode);

        //        foreach (var childNode in node.ChildNodes)
        //        {
        //            if (!TruncateNode(childNode, clonedNode.ChildNodes.ToList(), ref currentLength, maxLength))
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    return true;
        //}




        //public static string TruncateHtml(string input, int maxLength)
        //{
        //    if (string.IsNullOrEmpty(input) || maxLength <= 0)
        //        return string.Empty;

        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(input);

        //    if (doc.DocumentNode == null)
        //        return string.Empty;

        //    var currentLength = 0;
        //    var nodesToKeep = new List<HtmlNode>();

        //    foreach (var node in doc.DocumentNode.DescendantsAndSelf())
        //    {
        //        if (currentLength + node.InnerText.Length > maxLength)
        //        {
        //            break;
        //        }

        //        nodesToKeep.Add(node);
        //        currentLength += node.InnerText.Length;
        //    }

        //    var truncatedDoc = new HtmlDocument();
        //    if (nodesToKeep.Count > 0)
        //    {
        //        foreach (var node in nodesToKeep)
        //        {
        //            truncatedDoc.DocumentNode.AppendChild(node.Clone());
        //        }
        //        return truncatedDoc.DocumentNode.OuterHtml;
        //    }

        //    return string.Empty;
        //}

        
    }
}