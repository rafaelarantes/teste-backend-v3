﻿using ApprovalTests;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatricalPlayersRefactoringKata.Performances;
using TheatricalPlayersRefactoringKata.Tickets;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), 55),
            new Performance(new ComedyPlay("As You Like It", 2670), 35),
            new Performance(new TragedyPlay("Othello", 3560), 40),
            new Performance(new HistoryPlay("Henry V", 3227), 20),
            new Performance(new HistoryPlay("King John", 2648), 39),
            new Performance(new HistoryPlay("Henry V", 3227), 20),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();
        
        var result = statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), 55),
            new Performance(new ComedyPlay("As You Like It", 2670), 35),
            new Performance(new TragedyPlay("Othello", 3560), 40),
            new Performance(new HistoryPlay("Henry V", 3227), 20),
            new Performance(new HistoryPlay("King John", 2648), 39),
            new Performance(new HistoryPlay("Henry V", 3227), 20),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        var result = '﻿' + statementPrinter.PrintXml(invoice.GetStatementDto());

        //Assert
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), 55),
            new Performance(new ComedyPlay("As You Like It", 2670), 35),
            new Performance(new TragedyPlay("Othello", 3560), 40)
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        var result = statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy_RefatoredWithValues()
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), 55),
            new Performance(new ComedyPlay("As You Like It", 2670), 35),
            new Performance(new TragedyPlay("Othello", 3560), 40)
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert

        Assert.Equal(650, invoice.GetPerformancesByName("Hamlet").FirstOrDefault().Amount);
        Assert.Equal(547, invoice.GetPerformancesByName("As You Like It").FirstOrDefault().Amount);
        Assert.Equal(456, invoice.GetPerformancesByName("Othello").FirstOrDefault().Amount);

        Assert.Equal(1653, invoice.AmountOwed);
        Assert.Equal(47, invoice.EarnedCredits);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample_RefatoredWithValues()
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), 55),
            new Performance(new ComedyPlay("As You Like It", 2670), 35),
            new Performance(new TragedyPlay("Othello", 3560), 40),
            new Performance(new HistoryPlay("Henry V", 3227), 20),
            new Performance(new HistoryPlay("King John", 2648), 39),
            new Performance(new HistoryPlay("Henry V", 3227), 20),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        var result = statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(650M, invoice.GetPerformancesByName("Hamlet").FirstOrDefault().Amount);
        Assert.Equal(547M, invoice.GetPerformancesByName("As You Like It").FirstOrDefault().Amount);
        Assert.Equal(456M, invoice.GetPerformancesByName("Othello").FirstOrDefault().Amount);
        Assert.Equal(705.40M, invoice.GetPerformancesByName("Henry V").FirstOrDefault().Amount);
        Assert.Equal(931.60M, invoice.GetPerformancesByName("King John").FirstOrDefault().Amount);
        Assert.Equal(705.40M, invoice.GetPerformancesByName("Henry V")[1].Amount);

        Assert.Equal(3995.4M, invoice.AmountOwed);
        Assert.Equal(56, invoice.EarnedCredits);
    }


    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void PlayWithRangeGreater4000_Return4000()
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), 55),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert

        Assert.Equal(650, invoice.GetPerformancesByName("Hamlet").FirstOrDefault().Amount);

        Assert.Equal(650, invoice.AmountOwed);
        Assert.Equal(25, invoice.EarnedCredits);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void PlayWithRangeLess1000_Return1000()
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new ComedyPlay("As You Like It", 900), 35),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert

        Assert.Equal(380, invoice.GetPerformancesByName("As You Like It").FirstOrDefault().Amount);

        Assert.Equal(380, invoice.AmountOwed);
        Assert.Equal(12, invoice.EarnedCredits);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void ValueBaseIsLinesDivided10()
    {
        //Arrange
        const int LINES = 4000;

        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", LINES), 29)
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert

        Assert.Equal(LINES / 10, invoice.GetPerformancesByName("Hamlet").FirstOrDefault().Amount);

        Assert.Equal(400, invoice.AmountOwed);
        Assert.Equal(0, invoice.EarnedCredits);

    }

    [Theory]
    [InlineData(31)]
    [InlineData(32)]
    [UseReporter(typeof(DiffReporter))]
    public void AllPlays_WhenAudienceGreaterTo30_Sum1CreditForAdicionalAudience(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), audience),
        });

        StatementPrinter statementPrinter = new StatementPrinter();
        var volumeCredits = GetAdicionalAudienceTragedy(audience);

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(volumeCredits, invoice.EarnedCredits);
    }

    [Theory]
    [InlineData(30)]
    [InlineData(29)]
    [UseReporter(typeof(DiffReporter))]
    public void AllPlays_WhenAudienceLessOrEqualTo30_NotAddCreditsForAudience(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), audience),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(0, invoice.EarnedCredits);
    }

    [Theory]
    [InlineData(30)]
    [InlineData(29)]
    [UseReporter(typeof(DiffReporter))]
    public void TragedyPlay_WhenAudienceLessOrEqualTo30_AmountPlayWillBeBaseValue(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), audience),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(400, invoice.GetPerformancesByName("Hamlet").FirstOrDefault().Amount);
        Assert.Equal(400, invoice.AmountOwed);
    }


    [Theory]
    [InlineData(31)]
    [InlineData(32)]
    [UseReporter(typeof(DiffReporter))]
    public void TragedyPlay_WhenAudienceGreaterTo30_Sum10BaseValueForAdicionalAudience(int audience)
    {

        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new TragedyPlay("Hamlet", 4024), audience),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        var baseValue = 400 + 10 * (GetAdicionalAudienceTragedy(audience));

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(baseValue, invoice.GetPerformancesByName("Hamlet").FirstOrDefault().Amount);
        Assert.Equal(baseValue, invoice.AmountOwed);
    }

    [Theory]
    [InlineData(19)]
    [InlineData(20)]
    [UseReporter(typeof(DiffReporter))]
    public void AllComedyPlay_Sum3BaseValueForAudience(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new ComedyPlay("As You Like It", 2670), audience),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        var baseValue = GetBaseValueComedy(267, 0, audience);

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(baseValue, invoice.GetPerformancesByName("As You Like It").FirstOrDefault().Amount);
        Assert.Equal(baseValue, invoice.AmountOwed);
    }

    [Theory]
    [InlineData(19)]
    [InlineData(20)]
    [UseReporter(typeof(DiffReporter))]
    public void ComedyPlay_WhenAudienceLessOrEqualTo20_UseBaseValue(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new ComedyPlay("As You Like It", 2670), audience),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        var baseValue = 267 + audience * 3;

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(baseValue, invoice.GetPerformancesByName("As You Like It").FirstOrDefault().Amount);
        Assert.Equal(baseValue, invoice.AmountOwed);
    }


    [Theory]
    [InlineData(21)]
    [InlineData(22)]
    [UseReporter(typeof(DiffReporter))]
    public void ComedyPlay_WhenAudienceGreaterTo20_SumBaseValueWithAdicionalAudienceMultiplicationAdicionalValues(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new ComedyPlay("As You Like It", 2670), audience),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        var baseValue = GetBaseValueComedy(267, 100, audience) + 5 * GetAdicionalAudienceComedy(audience);

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(baseValue, invoice.GetPerformancesByName("As You Like It").FirstOrDefault().Amount);
        Assert.Equal(baseValue, invoice.AmountOwed);
    }

    [Theory]
    [InlineData(21)]
    [InlineData(22)]
    [UseReporter(typeof(DiffReporter))]
    public void ComedyPlay_AwaysAddComedyCredits_CalculteWithValuePartAudience(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new ComedyPlay("As You Like It", 2670), audience),
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        var volumeCredits = Math.Floor((decimal)audience / 5);
        volumeCredits += Math.Max(audience - 30, 0);

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(volumeCredits, invoice.EarnedCredits);
    }

    [Theory]
    [InlineData(19)]
    [InlineData(20)]
    [UseReporter(typeof(DiffReporter))]
    public void AllHistoryPlay_Sum3BaseValueForAudience(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new HistoryPlay("Henry V", 3227), audience)
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        var baseValue = GetBaseValueTragedy(322.7M) +
                        GetBaseValueComedy(322.7M, 0, audience);

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(baseValue, invoice.GetPerformancesByName("Henry V").FirstOrDefault().Amount);
        Assert.Equal(baseValue, invoice.AmountOwed);
    }

    [Theory]
    [InlineData(21)]
    [InlineData(22)]
    [UseReporter(typeof(DiffReporter))]
    public void HistoryPlay_WhenAudienceGreaterTo20_SumBaseValueWithAdicionalAudienceMultiplicationAdicionalValue(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new HistoryPlay("Henry V", 3227), audience)
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        var baseValue = GetBaseValueTragedy(322.7M) +
                        GetBaseValueComedy(322.7M, 100 + 5 * (audience - 20), audience);


        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(baseValue, invoice.GetPerformancesByName("Henry V").FirstOrDefault().Amount);
        Assert.Equal(baseValue, invoice.AmountOwed);
    }

        [Theory]
    [InlineData(31)]
    [InlineData(32)]
    [UseReporter(typeof(DiffReporter))]
    public void HistoryPlay_WhenAudienceGreaterTo30_AdicionalAudienceMultiplicationAdicionalValue(int audience)
    {
        //Arrange
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(new HistoryPlay("Henry V", 3227), audience)
        });

        StatementPrinter statementPrinter = new StatementPrinter();

        var baseValue = GetBaseValueTragedy(322.7M, 10 * (audience - 30)) +
                        GetBaseValueComedy(322.7M, 100 + 5 * (audience - 20), audience);

        //Act
        invoice.Calculute();

        statementPrinter.Print(invoice.GetStatementDto());

        //Assert
        Assert.Equal(baseValue, invoice.GetPerformancesByName("Henry V").FirstOrDefault().Amount);
        Assert.Equal(baseValue, invoice.AmountOwed);
    }

    private decimal GetBaseValueTragedy(decimal baseValue, decimal? adicionalValue = 0)
    {
        return baseValue + adicionalValue.Value;
    }

    private decimal GetBaseValueComedy(decimal baseValue, decimal adicionalValues, decimal audience)
    {
        return baseValue + adicionalValues + 3 * audience;
    }

    private decimal GetAdicionalAudienceComedy(decimal audience)
    {
        return audience - 20;
    }

    private decimal GetAdicionalAudienceTragedy(decimal audience)
    {
        return audience - 30;
    }

}
