﻿namespace TestTask.DTO;

public class OrderResponseDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public int Prioriry { get; set; }
    public decimal TotalAmountInBaseCurrency { get; set; }
}