﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace P010Store.Data.Absract
{
    public interface IRepository<T> where T : class// IRepository interface i Entity lerimiz için gerçekleştireceğimiz veritabanı işlemlerini yapacak olan Repository class ında bulunması gereken metotların imzalarını tutuyor. <T> kodu bu interface e dışarıdan parametre olarak generic bir nesnesinin gönderilebilmesini sağlar. where T : class kodu ise T nin tipinin class olması gerektiğini belirler, böylece string gibi bir veri gönderilmeye kalkılırsa interface bunu kabul etmeyecektir.
												   //[Guid("4A66838C-B9DB-4F3D-96F3-4AC4CBD0253D")]
	{
		List<T> GetAll(Expression<Func<T, bool>> expression); // Get metodunda entity frazmework x=>x. şeklinde yaptığımız lamda experesion larınını kullabilmek için 
        T Get(Expression<Func<T, bool>> expression);// Özel sorgu kullanarak 1 tane kayıt getiren metot imzası 
        T Find(int id);
        int Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();
        // Asekron metotlar
        Task<T> FindAsync(int id);
        Task<T> FirstOfDefaultAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAllAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task<int> SaveChangesAsync();

       
    }
}
