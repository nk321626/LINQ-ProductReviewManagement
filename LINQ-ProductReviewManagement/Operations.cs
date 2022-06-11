using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_ProductReviewManagement
{
    public class Operations
    {
        DataTable dataTable = new DataTable();
        public void GetTop3Records(List<ProductReview> list)
        {
            var result = list.OrderByDescending(x => x.Rating).Take(3).ToList();
            Display(result);
        }
        public void Display(List<ProductReview> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.ProductId + "\t" + item.UserId + "\t" + item.Rating + "\t" + item.Review + "\t" + item.IsLike);
                Console.WriteLine(" ");
            }
        }
        public void RetriveRecordsWithRatingAndProductID(List<ProductReview> list)
        {
            var result = list.Where(x => x.Rating > 3 && (x.ProductId == 1 || x.ProductId == 4 || x.ProductId == 9)).Take(3).ToList();
            Display(result);
        }
        public void RetriveRecordsCount(List<ProductReview> list)
        {
            var result = list.GroupBy(x => x.ProductId).Select(x => new { ProductID = x.Key, Count = x.Count() });
            foreach (var item in result)
            {
                Console.WriteLine(item.ProductID + " " + item.Count);
            }
        }
        public void RetriveProductIDAndReviewOfAllRecords(List<ProductReview> list)
        {
            var result = list.OrderBy(x => x.ProductId).Select(x => new { productId = x.ProductId, Review = x.Review });
            foreach (var item in result)
            {
                Console.WriteLine(item.productId + " " + item.Review);
            }
        }
        public void SkipTop5Records(List<ProductReview> list)
        {
            var result = list.OrderBy(x => x.Rating).Skip(5).ToList();
            Display(result);
        }
        public void CreateDataTable(List<ProductReview> list)
        {
            //Adding columns to data table and their type
            dataTable.Columns.Add("ProductId", typeof(int));
            dataTable.Columns.Add("UserId", typeof(int));
            dataTable.Columns.Add("Rating", typeof(double));
            dataTable.Columns.Add("Review", typeof(string));
            dataTable.Columns.Add("IsLike", typeof(bool));

            //Adding values to datatable from the list
            foreach (var data in list)
            {
                dataTable.Rows.Add(data.ProductId, data.UserId, data.Rating, data.Review, data.IsLike);
            }
            Console.WriteLine("Successfully added values to datatable");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("ProductID: {0}\t UserID: {1}\t Review: {2}\t Rating: {3}\t IsLike: {4}\t", row[0], row[1], row[2], row[3], row[4]);
            }
        }
        public void RetriveRecordsForIsTrue(List<ProductReview> list)
        {
            CreateDataTable(list);
            var result = from table in dataTable.AsEnumerable() where table.Field<bool>("IsLike") == true select table;
            Console.WriteLine($"ProductId,  UserId,  Rating,  Review,  IsLike");
            foreach (var row in result)
            {
                Console.WriteLine($"{row["ProductId"]},  {row["UserId"]},  {row["Rating"]},  {row["Review"]},  {row["IsLike"]}");
            }
        }
        public void AverageRating(List<ProductReview> list)
        {
            var result = list.GroupBy(x => x.ProductId).Select(x => new { productId = x.Key, Rating = x.Average(t => t.Rating) });
            foreach (var item in result)
            {
                Console.WriteLine(item.productId + " " + item.Rating);
            }
        }
    }
}
