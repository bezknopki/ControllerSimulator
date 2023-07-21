using ControllerSimulator.DataAccess;
using ControllerSimulator.Helpers;
using ControllerSimulator.Models;
using System.Configuration;

namespace ControllerSimulator.Controllers
{
    /*
    * ТЕСТОВОЕ ЗАДАНИЕ
    * 
    * Исходные:
    * В нашем распоряжении имеется гипотетический генератор данных DataSourceSimulator,
    * выполняющий роль СУБД. Генератор выгружает всех заказчиков статическим методом
    * GetAllCustomers в массив структур RoughCustomer. Класс DataSourceSimulator и
    * структура RoughCustomer являются завершёнными, изменять их каким-либо образом
    * не требуется. Таким образом следует использовать DataSourceSimulator.GetAllCustomers
    * как источник данных.
    * 
    * В нашем распоряжении пустая симуляция гипотетического контроллера Web API и пустой
    * класс Customer, возвращаемый контроллером. WebApiControllerSimulator - симуляция
    * ASP.NET Core Web API, исполняемого в стилистике REST, и использующего JSON в качестве
    * формата передаваемых данных. Мы должны заполнить класс Customer и веб-методы класса
    * WebApiControllerSimulator.
    * 
    * Задание:
    * 1. Создать класс Customer для серверных операций и для передачи данных в формате
    * JSON.
    * 2. Реализовать все методы гипотетического контроллера WebApiControllerSimulator в
    * соотвествии с заявленной в комментариях функциональностью.
    * 
    * Замечания:
    * 1. Создание класса Customer полностью на наше усмотрение.
    * 2. Реализация веб-методов класса WebApiControllerSimulator подразумевает только
    * проектирование тел веб-методов. Определять атрибуты класса и методов не требуется.
    * Также не требуется возвращать ActionResult и поддерживать HTTP status codes. Мы
    * просто возвращаем требуемые данные там где это требуется в соотвествии с сигнатурой
    * веб-метода.
    * 3. Можно создавать дополнительные классы и прочие типы, если это необходимо.
    * 4. Волне допускается тестирование созданных решений. Поскольку класс
    * WebApiControllerSimulator является всего лишь симуляцией контроллера Web API
    * и может быть не учень удобным для тестирования, допускается оформление этого
    * класса просто как класса, каково оно и де-факто есть, чьи методы просто
    * вызываются, содержащей его программой.
    * 5. Данное задание не требует от нас решений, обеспечивающих маскимальную
    * производительность, при том что проблематику производительности и масштабируемости
    * следует учитывать при решении данного задания. Скорее требуется написать качественный
    * и хорошо-читаемый прикладной код.
    */

    /// <summary>
    /// Our simulated Web API controller.
    /// </summary>
    public class WebApiControllerSimulator
    {
        UnitOfWork unitOfWork;
        string dumpPath;
        public WebApiControllerSimulator(UnitOfWork uow)
        {
            unitOfWork = uow;
            dumpPath = "..\\Dumps";
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Returns all customers or nothing in case of any error.</returns>
        public async Task<IEnumerable<Customer>> GetAllCustomers(CancellationToken ct)
        {
            return await Task.Run(
                () =>
                unitOfWork.GetAllCustomers()
                , ct);
        }

        /// <summary>
        /// Finds specific customer.
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="iCustomerId"></param>
        /// <returns>Returns found customer or null.</returns>
        public async Task<Customer?> FindCustomer(CancellationToken ct, int iCustomerId)
        {
            return await Task.Run(
                () =>
                unitOfWork.GetCustomer(iCustomerId)
                , ct);
        }

        /// <summary>
        /// Finds all customers older than the specified date.
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="dt">The specified date.</param>
        /// <returns>Returns all customers older than the specified date or nothing in case of any error.</returns>
        public async Task<IEnumerable<Customer>> FindOlder(CancellationToken ct, DateTime dt)
        {
            return await Task.Run(
                () =>
                unitOfWork.GetCustomersOlderThan(dt)
                , ct);
        }

        /// <summary>
        /// Produces total quota (sum) of all customers.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Total quota or -1 in case of any error.</returns>
        public async Task<int> GetTotalQuota(CancellationToken ct)
        {
            return await Task.Run(
                () =>
                unitOfWork.GetTotalQuota()
                , ct);
        }

        /// <summary>
        /// Dumps the customer identified by the specified customer ID to disk into a JSON (.jsonc) file.
        /// Does nothing in case of any error.
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="iCustomerId">Customer ID.</param>
        /// <returns></returns>
        public async Task DumpCustomer(CancellationToken ct, int iCustomerId)
        {
            await Task.Run(
                () =>
                {
                    var customer = unitOfWork.GetCustomer(iCustomerId);
                    if (customer != null)
                        DumpHelper.DumpCustomer(customer);
                }
                , ct);
            //first find the customer by iCustomerId.
            //then save it anywhere (doesn't matter where) to disk with
            //System.IO.File/FileStream/StreamWriter class.
        }

        /// <summary>
        /// Dumps full names of all customers to disk into a text (.txt) file.
        /// The file will contain lines of full names, one full name per one line.
        /// Does nothing in case of any error.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task DumpAllFullNames(CancellationToken ct)
        {
            await Task.Run(
                () =>
                {
                    var customers = unitOfWork.GetAllCustomers();
                    DumpHelper.DumpCustomersFullName(customers);
                }
                , ct);
            //full name = first name + ' ' + middle name + ' ' + last name

            //save full names of all customers anywhere (doesn't matter where) to disk
            //with System.IO.File/FileStream/StreamWriter class.
        }

        /// <summary>
        /// Updates quotas of all customers and returns all customers with the quotas updated.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>All customers with the quotas updated or nothing in case of any error.</returns>
        public async Task<IEnumerable<Customer>> UpdateAllQuotas(CancellationToken ct)
        {
            return await Task.Run(
                () =>
                unitOfWork.UpdateAllQuotas()
                , ct);
            //assume that there's some "hypotetic procedure" of "quota update"
            //existing for our customers.

            //the quota update algorithm is the following:
            //if regular: update quota by +1;
            //if valuable: update quota by +2.
            //if vip: update quota by +5.
        }
    }
}
