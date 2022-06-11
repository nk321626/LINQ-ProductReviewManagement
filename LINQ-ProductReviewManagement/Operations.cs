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
        //Create Method to Get Top3 from Records (UC1)
        public void GetTop3Records(List<ProductReview> list)
        {
            var result = list.OrderByDescending(x => x.Rating).Take(3).ToList();
            Display(result);
        }
        //Create Method to Display Records (UC2)
        public void Display(List<ProductReview> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.ProductId + "\t" + item.UserId + "\t" + item.Rating + "\t" + item.Review + "\t" + item.IsLike);
                Console.WriteLine(" ");
            }
        }
        //Create Method to Retrive Records with rating and Product ID (UC3)
        public void RetriveRecordsWithRatingAndProductID(List<ProductReview> list)
        {
            var result = list.Where(x => x.Rating > 3 && (x.ProductId == 1 || x.ProductId == 4 || x.ProductId == 9)).Take(3).ToList();
            Display(result);
        }
        //Create Method to Retrive Records Count of review (UC4)
        public void RetriveRecordsCount(List<ProductReview> list)
        {
            var result = list.GroupBy(x => x.ProductId).Select(x => new { ProductID = x.Key, Count = x.Count() });
            foreach (var item in result)
            {
                Console.WriteLine(item.ProductID + " " + item.Count);
            }
        }
        //Create Method to Retrive Product ID And Review Of AllRecords (UC5)
        public void RetriveProductIDAndReviewOfAllRecords(List<ProductReview> list)
        {
            var result = list.OrderBy(x => x.ProductId).Select(x => new { productId = x.ProductId, Review = x.Review });
            foreach (var item in result)
            {
                Console.WriteLine(item.productId + " " + item.Review);
            }
        }
        //Create Method to Skip Top 5 (UC6)
        public void SkipTop5Records(List<ProductReview> list)
        {
            var result = list.OrderBy(x => x.Rating).Skip(5).ToList();
            Display(result);
        }
        //Create Method to create table (UC8)
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
        //Create Method to Retrive Records for is True (UC9)
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
        //Create Method to Average Rating (UC10)
        public void AverageRating(List<ProductReview> list)
        {
            var result = list.GroupBy(x => x.ProductId).Select(x => new { productId = x.Key, Rating = x.Average(t => t.Rating) });
            foreach (var item in result)
            {
                Console.WriteLine(item.productId + " " + item.Rating);
            }
        }
        //Create Method to Get Record For Nice Review (UC11)
        public void GetRecordForNiceReview(List<ProductReview> list)
        {
            var result = (from productReviews in list where productReviews.Review == "Nice" select productReviews);
            foreach (var data in result)
            {
                Console.WriteLine("ProductID:- " + data.ProductId + " " + "UserID:- " + data.UserId + " " + "Rating:- " + data.Rating + " " + "Review:- " + data.Review + " " + "IsLike:- " + data.IsLike);
            }
        }
        //create Method to Get Record for User10 (UC12)
        public void GetRecordsForUser10(List<ProductReview> list)
        {
            var result = (from productReviews in list where productReviews.UserId == 10 select productReviews).OrderBy(x => x.Rating);
            foreach (var data in result)
            {
                Console.WriteLine("ProductID:- " + data.ProductId + " " + "UserID:- " + data.UserId + " " + "Rating:- " + data.Rating + " " + "Review:- " + data.Review + " " + "IsLike:- " + data.IsLike);
            }
        }
    }
}
