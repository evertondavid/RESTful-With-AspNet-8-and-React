using Microsoft.AspNetCore.Mvc;

// Namespace for the Calculator API
namespace CalculatorAPI.Controllers;

// Attribute to indicate that this class is a controller
[ApiController]
// Route attribute to configure the URL pattern for this controller
[Route("[controller]")]
public class PersonController : ControllerBase
{
    // Logger instance for logging
    private readonly ILogger<PersonController> _logger;

    // Constructor that accepts a logger
    public PersonController(ILogger<PersonController> logger)
    {
        _logger = logger;
    }

    // GET method for the Person API
    // The route is api/Person/{operatorType}/{firstNumber}/{secondNumber}
    [HttpGet("{operatorType}/{firstNumber}/{secondNumber}")]
    public IActionResult Get(String operatorType, string firstNumber, string secondNumber)
    {
        // Check if the input is a number or not
        // If both inputs are numbers, perform the math operation
        if (decimal.TryParse(firstNumber, out decimal first) && decimal.TryParse(secondNumber, out decimal second))
        {
            return Ok(MathOperation(operatorType, first, second));
        }
        // If the input is not a number return a bad request
        return BadRequest("Invalid Input");
    }

    // Private method to perform the math operation based on the operator type
    // Supports addition (+), subtraction (-), multiplication (*), division (/), average (avg), and square root (sqrt)
    private decimal MathOperation(string operatorType, decimal firstNumber, decimal secondNumber)
    {
        switch (operatorType.ToLower())
        {
            case "+":
                return firstNumber + secondNumber;
            case "-":
                return firstNumber - secondNumber;
            case "*":
                return firstNumber * secondNumber;
            case "/":
                return firstNumber / secondNumber;
            case "avg":
                return (firstNumber + secondNumber) / 2;
            case "sqrt":
                return (decimal)Math.Sqrt((double)firstNumber);
            default:
                return 0;
        }
    }
}
