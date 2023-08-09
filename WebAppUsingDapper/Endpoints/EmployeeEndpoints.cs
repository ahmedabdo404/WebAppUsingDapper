using Dapper;
using Microsoft.Data.SqlClient;
using WebAppUsingDapper.Models;
using WebAppUsingDapper.Services;

namespace WebAppUsingDapper.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static void MapEmployeeEndpoint(this IEndpointRouteBuilder builder)
        {
            var endPointGroup = builder.MapGroup("Employee");

            endPointGroup.MapGet("GetAll", async (DbConnectionFactory dbConnectionFactory) =>
            {
                using SqlConnection connection = dbConnectionFactory.Create();
                const string query = "SELECT * FROM Employees";
                var employees = await connection.QueryAsync<Employee>(query);

                return Results.Ok(employees);
            });

            endPointGroup.MapGet("GetById/{id}", async (int id, DbConnectionFactory dbConnectionFactory) =>
            {
                using SqlConnection connection = dbConnectionFactory.Create();
                const string query = "SELECT * FROM Employees WHERE Id = @EmployeeId";
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(query,
                    param: new
                    {
                        EmployeeId = id
                    });

                if(employee is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(employee);
            });

            endPointGroup.MapPost("Add", async (Employee employee, DbConnectionFactory dbConnectionFactory) =>
            {
                using SqlConnection connection = dbConnectionFactory.Create();
                const string query =
                """
                    INSERT INTO Employees(Name, Age, Salary, DeptId)
                    VALUES(@Name, @Age, @Salary, @DeptId)
                """;
                await connection.ExecuteAsync(query, employee);

                return Results.Ok(employee);
            });

            endPointGroup.MapPut("Update", async (Employee employee, DbConnectionFactory dbConnectionFactory) =>
            {
                using SqlConnection connection = dbConnectionFactory.Create();
                const string getEmployeeQuery = "SELECT * FROM Employees WHERE Id = @EmployeeId";
                var checkEmployee = await connection.QuerySingleOrDefaultAsync<Employee>(getEmployeeQuery,
                    param: new
                    {
                        EmployeeId = employee.Id
                    });

                if (checkEmployee is null)
                {
                    return Results.NotFound();
                }

                const string UpdateQuery =
                """
                    UPDATE Employees
                    SET Name = @Name,
                        Age = @Age,
                        Salary = @Salary,
                        DeptId = @DeptId
                    WHERE Id = @Id
                """;
                await connection.ExecuteAsync(UpdateQuery, employee);

                return Results.Ok(employee);
            });

            endPointGroup.MapDelete("Delete", async (int id, DbConnectionFactory dbConnectionFactory) =>
            {
                using SqlConnection connection = dbConnectionFactory.Create();
                const string getEmployeeQuery = "SELECT * FROM Employees WHERE Id = @EmployeeId";
                var checkEmployee = await connection.QuerySingleOrDefaultAsync<Employee>(getEmployeeQuery,
                    param: new
                    {
                        EmployeeId = id
                    });

                if (checkEmployee is null)
                {
                    return Results.NotFound();
                }

                const string query =
                """
                    DELETE FROM Employees
                    Where Id = @Id
                """;
                await connection.ExecuteAsync(query, param: new {Id = id});

                return Results.Ok("Deleted");
            });
        }
    }
}
