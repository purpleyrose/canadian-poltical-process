using Godot;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

public partial class PolicyScoreCalculator : Node
{

	private Label SocialScoreLabel;
	private Label EconomicScoreLabel;
	private Label QuebecScoreLabel;
	private Label EnvironmentalScoreLabel;
	private Label SuggestedPartyLabel;

	private readonly Dictionary<string, string> PolicyCategories = new Dictionary<string, string> // The 4 categories are Social, Economic, Quebec Indepndence and Environmental
	{
		{"WealthTax", "Economic"},
		{"FinancialLiteracyClasses", "Social"},
		{"RehibilationPrograms", "Social"},
		{"AffordableHousing", "Social"},
		{"HighSpeedRail", "Social"},
		{"WaterInfrastrucutreUpgrades", "Social"},
		{"MinimumWage", "Economic"},
		{"UnionFunding", "Economic"},
		{"RightToWork", "Economic"},
		{"UBI", "Economic"},
		{"SmallBuisnessSubsidies", "Economic"},
		{"PublicOwnershipOfMajorIndustries", "Economic"},
		{"WealthyTaxCuts", "Economic"},
		{"Deregulation", "Economic"},
		{"TransactionTax", "Economic"},
		{"CarbonTax", "Economic"},
		{"FreeTrade", "Economic"},
		{"PharmaCare", "Social"},
		{"FreeMentalHealth", "Social"},
		{"RareDeseaseResearch", "Social"},
		{"DrugDecrim", "Social"},
		{"DrugPriceCaps", "Social"},
		{"LongTermCareFunding", "Social"},
		{"UniversalHealthCare", "Social"},
		{"FreeUniversityTuition", "Social"},
		{"PublicSchoolFunding", "Social"},
		{"SchoolVouchers", "Social"},
		{"StudentLoanElim", "Social"},
		{"NationalCurriculum", "Social"},
		{"TeacherSalaryIncrease", "Social"},
		{"FinacialLiteracyClasses", "Social"},
		{"STEMFunding", "Social"},
		{"PrivateSchoolBan", "Social"},
		{"SingleUsePlasticsBan", "Environmental"},
		{"RenewableEnergyInvestements", "Environmental"},
		{"FossileFuelPhaseOut", "Environmental"},
		{"NationalParkExpansion", "Environmental"},
		{"2050CarbonNeutrality", "Environmental"},
		{"EVSubsidies", "Environmental"},
		{"PolutionRegs", "Environmental"},
		{"NuclearEnergy", "Environmental"},
		{"FrackingBan", "Environmental"},
		{"UniversalChildCare", "Social"},
		{"GunControl", "Social"},
		{"Abortion", "Social"},
		{"GayMarriage", "Social"},
		{"GenderNeutralRestrooms", "Social"},
		{"ConversionTherapyBan", "Social"},
		{"InformedConsent", "Social"},
		{"LegalSexWork", "Social"},
		{"PaidParentalLeave", "Social"},
		{"IndigenousFunding", "Social"},
		{"PoliceFundingIncrease", "Social"},
		{"MandatoryBodyCams", "Social"},
		{"CriminalJusticeReform", "Social"},
		{"MinimumSentencing", "Social"},
		{"DeathPenalty", "Social"},
		{"PrivatePrisonBan", "Social"},
		{"IncreasedSurveilance", "Social"},
		{"OpenBorders", "Social"},
		{"IncreasedFundingForImmigration", "Social"},
		{"BorderWall", "Social"},
		{"PathwayToCitizenship", "Social"},
		{"MassDeportation", "Social"},
		{"RefugeeResettlementProgram", "Social"},
		{"PointsBasedImmigration", "Social"},
		{"BanFromSpecific", "Social"},
		{"PublicTransportPolicy", "Social"},
		{"GovernmentHighSpeedInternet", "Social"},
		{"HighwayExpansion", "Social"},
		{"WaterInfrastructureUpgrades", "Social"},
		{"UrbanRenewal", "Social"},
		{"Withdrawl", "Social"},
		{"IncreasedDefenseSpending", "Social"},
		{"NATOSupport", "Social"},
		{"EconomicSanctions", "Social"},
		{"HumanatarianAid", "Social"},
		{"ClimateTreaties", "Social"},
		{"MilataryAlliances", "Social"},
		{"TradeEmbargoes", "Economic"},
		{"AIResearchFunding", "Social"},
		{"BigTechReg", "Social"},
		{"FacialTechBan", "Social"},
		{"InternetPrivacyLaws", "Social"},
		{"GreenTechTaxCredit", "Environmental"},
		{"UniversalInternet", "Social"},
		{"CryptoAdoption", "Economic"},
		{"LowerIncomeTax", "Economic"},
		{"MiddleIncomeTax", "Economic"},
		{"HighIncomeTax", "Economic"},
		{"FlatIncomeTax", "Economic"},
		{"CorprateTax", "Economic"},
		{"SalesTax", "Economic"},
		{"LuxuryGoodsTax", "Economic"},
		{"InherentenceTax", "Economic"},
		{"CapitalGainsTax", "Economic"},
		{"QuebecIndependence", "Quebec"},
		{"Bilingualism", "Quebec"}
	};
	private readonly Dictionary<string, double> PolicyWeights = new Dictionary<string, double>
	{
		{"WealthTax", -0.5},
		{"FinancialLiteracyClasses", +0.01},
		{"RehibilationPrograms", -0.25},
		{"AffordableHousing", -0.5},
		{"HighSpeedRail", -0.25},
		{"WaterInfrastrucutreUpgrades", -0.25},
		{"MinimumWage", -0.9},
		{"UnionFunding", -2},
		{"RightToWork", +1},
		{"UBI", -0.5},
		{"SmallBuisnessSubsidies", +0.5},
		{"PublicOwnershipOfMajorIndustries", -5},
		{"WealthyTaxCuts", +2},
		{"Deregulation", +2},
		{"TransactionTax", -0.5},
		{"CarbonTax", -0.1},
		{"FreeTrade", +0.4},
		{"PharmaCare", -0.5},
		{"FreeMentalHealth", -0.5},
		{"RareDeseaseResearch", -0.5},
		{"DrugDecrim", -1},
		{"DrugPriceCaps", -0.5},
		{"LongTermCareFunding", -0.5},
		{"UniversalHealthCare", -0.6},
		{"FreeUniversityTuition", -0.5},
		{"PublicSchoolFunding", -0.5},
		{"SchoolVouchers", +0.5},
		{"StudentLoanElim", -0.5},
		{"NationalCurriculum", -0.2},
		{"TeacherSalaryIncrease", -0.2},
		{"FinacialLiteracyClasses", +0.01},
		{"STEMFunding", +0.01},
		{"PrivateSchoolBan", -0.7},
		{"SingleUsePlasticsBan", -1.5},
		{"RenewableEnergyInvestements", -0.2},
		{"FossileFuelPhaseOut", -2},
		{"NationalParkExpansion", -0.5},
		{"2050CarbonNeutrality", -0.8},
		{"EVSubsidies", -0.2},
		{"PolutionRegs", -1.8},
		{"NuclearEnergy", -0.8},
		{"FrackingBan", -2},
		{"UniversalChildCare", -0.4},
		{"GunControl", -0.5},
		{"Abortion", -2.5},
		{"GayMarriage", -1.5},
		{"GenderNeutralRestrooms", -1.5},
		{"ConversionTherapyBan", -1.5},
		{"InformedConsent", -1},
		{"LegalSexWork", -2},
		{"PaidParentalLeave", -0.5},
		{"IndigenousFunding", -0.5},
		{"PoliceFundingIncrease", +0.5},
		{"MandatoryBodyCams", -0.5},
		{"CriminalJusticeReform", -1.5},
		{"MinimumSentencing", +1.5},
		{"DeathPenalty", +2},
		{"PrivatePrisonBan", -2},
		{"IncreasedSurveilance", +4},
		{"OpenBorders", -0.5},
		{"IncreasedFundingForImmigration", -1},
		{"BorderWall", +2},
		{"PathwayToCitizenship", -1},
		{"MassDeportation", +5},
		{"RefugeeResettlementProgram", +1},
		{"PointsBasedImmigration", +0.1},
		{"BanFromSpecific", +4},
		{"PublicTransportPolicy", -1},
		{"GovernmentHighSpeedInternet", -2},
		{"HighwayExpansion", +0.01},
		{"WaterInfrastructureUpgrades", -2},
		{"UrbanRenewal", -3},
		{"Withdrawl", -1},
		{"IncreasedDefenseSpending", +1},
		{"NATOSupport", +0.5},
		{"EconomicSanctions", -0.5},
		{"HumanatarianAid", -1},
		{"ClimateTreaties", -1},
		{"MilataryAlliances", +0.5},
		{"TradeEmbargoes", -0.5},
		{"AIResearchFunding", -0.01},
		{"BigTechReg", -2},
		{"FacialTechBan", -4},
		{"InternetPrivacyLaws", -5},
		{"GreenTechTaxCredit", -0.2},
		{"UniversalInternet", -0.5},
		{"CryptoAdoption", +2},
		{"LowerIncomeTax", -2},
		{"MiddleIncomeTax", -2},
		{"HighIncomeTax", -2},
		{"FlatIncomeTax", +4},
		{"CorprateTax", -2},
		{"SalesTax", +1},
		{"LuxuryGoodsTax", -3},
		{"InherentenceTax", -2},
		{"CapitalGainsTax", -3},
		{"QuebecIndependence", -6},
		{"Bilingualism", -4}
	};

	private readonly Dictionary<string, Dictionary<string, object>> PartyPolicies = new Dictionary<string, Dictionary<string, object>>
	{
		{
			"LPC", new Dictionary<string, object> // We will use the current party leader's policies as a stand in for the party's policies
			{
				{"WealthTax", false},
				{"FinancialLiteracyClasses", true},
				{"RehibilationPrograms", true},
				{"AffordableHousing", true},
				{"HighSpeedRail", true},
				{"MinimumWage", true},
				{"UnionFunding", true},
				{"RightToWork", false},
				{"UBI", false},
				{"SmallBuisnessSubsidies", true},
				{"PublicOwnershipOfMajorIndustries", false},
				{"WealthyTaxCuts", false},
				{"Deregulation", false},
				{"TransactionTax", false},
				{"CarbonTax", true},
				{"FreeTrade", true},
				{"PharmaCare", true},
				{"FreeMentalHealth", false},
				{"RareDeseaseResearch", true},
				{"DrugDecrim", false},
				{"DrugPriceCaps", true},
				{"LongTermCareFunding", true},
				{"UniversalHealthCare", true},
				{"FreeUniversityTuition", false},
				{"PublicSchoolFunding", true},
				{"SchoolVouchers", false},
				{"StudentLoanElim", false},
				{"NationalCurriculum", true},
				{"TeacherSalaryIncrease", true},
				{"STEMFunding", true},
				{"PrivateSchoolBan", false},
				{"SingleUsePlasticsBan", true},
				{"RenewableEnergyInvestements", true},
				{"FossileFuelPhaseOut", true},
				{"NationalParkExpansion", true},
				{"2050CarbonNeutrality", true},
				{"EVSubsidies", true},
				{"PolutionRegs", true},
				{"NuclearEnergy", true},
				{"FrackingBan", false},
				{"UniversalChildCare", true},
				{"GunControl", true},
				{"Abortion", true},
				{"GayMarriage", true},
				{"GenderNeutralRestrooms", true},
				{"ConversionTherapyBan", true},
				{"InformedConsent", true},
				{"LegalSexWork", true},
				{"PaidParentalLeave", true},
				{"IndigenousFunding", true},
				{"PoliceFundingIncrease", true},
				{"MandatoryBodyCams", true},
				{"CriminalJusticeReform", true},
				{"MinimumSentencing", false},
				{"DeathPenalty", false},
				{"PrivatePrisonBan", true},
				{"IncreasedSurveilance", false},
				{"OpenBorders", false},
				{"IncreasedFundingForImmigration", true},
				{"BorderWall", false},
				{"PathwayToCitizenship", true},
				{"MassDeportation", false},
				{"RefugeeResettlementProgram", true},
				{"PointsBasedImmigration", true},
				{"BanFromSpecific", false},
				{"PublicTransportPolicy", true},
				{"GovernmentHighSpeedInternet", true},
				{"HighwayExpansion", true},
				{"WaterInfrastructureUpgrades", true},
				{"UrbanRenewal", true},
				{"Withdrawl", false},
				{"IncreasedDefenseSpending", true},
				{"NATOSupport", true},
				{"EconomicSanctions", true},
				{"HumanatarianAid", true},
				{"ClimateTreaties", true},
				{"MilataryAlliances", true},
				{"TradeEmbargoes", true},
				{"AIResearchFunding", true},
				{"BigTechReg", true},
				{"FacialTechBan", true},
				{"InternetPrivacyLaws", true},
				{"GreenTechTaxCredit", true},
				{"UniversalInternet", true},
				{"CryptoAdoption", false},
				{"LowerIncomeTax", "Maintain"},
				{"MiddleIncomeTax", "Increase"},
				{"HighIncomeTax", "Increase"},
				{"FlatIncomeTax", false},
				{"CorprateTax", "Increase"},
				{"SalesTax", false},
				{"LuxuryGoodsTax", true},
				{"InherentenceTax", true},
				{"CapitalGainsTax", true},
				{"QuebecIndependence", false},
				{"Bilingualism", true}
			}

		},
		{
			"NDP", new Dictionary<string, object>
			{
				{"WealthTax", true},
				{"FinancialLiteracyClasses", true},
				{"RehibilationPrograms", true},
				{"AffordableHousing", true},
				{"HighSpeedRail", true},
				{"MinimumWage", true},
				{"UnionFunding", true},
				{"RightToWork", false},
				{"UBI", true},
				{"SmallBuisnessSubsidies", true},
				{"PublicOwnershipOfMajorIndustries", true},
				{"WealthyTaxCuts", false},
				{"Deregulation", false},
				{"TransactionTax", true},
				{"CarbonTax", true},
				{"FreeTrade", false},
				{"PharmaCare", true},
				{"FreeMentalHealth", true},
				{"RareDeseaseResearch", true},
				{"DrugDecrim", true},
				{"DrugPriceCaps", true},
				{"LongTermCareFunding", true},
				{"UniversalHealthCare", true},
				{"FreeUniversityTuition", true},
				{"PublicSchoolFunding", true},
				{"SchoolVouchers", false},
				{"StudentLoanElim", true},
				{"NationalCurriculum", true},
				{"TeacherSalaryIncrease", true},
				{"STEMFunding", true},
				{"PrivateSchoolBan", true},
				{"SingleUsePlasticsBan", true},
				{"RenewableEnergyInvestements", true},
				{"FossileFuelPhaseOut", true},
				{"NationalParkExpansion", true},
				{"2050CarbonNeutrality", true},
				{"EVSubsidies", true},
				{"PolutionRegs", true},
				{"NuclearEnergy", false},
				{"FrackingBan", true},
				{"UniversalChildCare", true},
				{"GunControl", true},
				{"Abortion", true},
				{"GayMarriage", true},
				{"GenderNeutralRestrooms", true},
				{"ConversionTherapyBan", true},
				{"InformedConsent", true},
				{"LegalSexWork", true},
				{"PaidParentalLeave", true},
				{"IndigenousFunding", true},
				{"PoliceFundingIncrease", false},
				{"MandatoryBodyCams", true},
				{"CriminalJusticeReform", true},
				{"MinimumSentencing", false},
				{"DeathPenalty", false},
				{"PrivatePrisonBan", true},
				{"IncreasedSurveilance", false},
				{"OpenBorders", false},
				{"IncreasedFundingForImmigration", true},
				{"BorderWall", false},
				{"PathwayToCitizenship", true},
				{"MassDeportation", false},
				{"RefugeeResettlementProgram", true},
				{"PointsBasedImmigration", false},
				{"BanFromSpecific", false},
				{"PublicTransportPolicy", true},
				{"GovernmentHighSpeedInternet", true},
				{"HighwayExpansion", false},
				{"WaterInfrastructureUpgrades", true},
				{"UrbanRenewal", true},
				{"Withdrawl", false},
				{"IncreasedDefenseSpending", false},
				{"NATOSupport", false},
				{"EconomicSanctions", true},
				{"HumanatarianAid", true},
				{"ClimateTreaties", true},
				{"MilataryAlliances", false},
				{"TradeEmbargoes", false},
				{"AIResearchFunding", true},
				{"BigTechReg", true},
				{"FacialTechBan", true},
				{"InternetPrivacyLaws", true},
				{"GreenTechTaxCredit", true},
				{"UniversalInternet", true},
				{"CryptoAdoption", false},
				{"LowerIncomeTax", "Maintain"},
				{"MiddleIncomeTax", "Increase"},
				{"HighIncomeTax", "Increase"},
				{"FlatIncomeTax", false},
				{"CorprateTax", "Increase"},
				{"SalesTax", false},
				{"LuxuryGoodsTax", true},
				{"InherentenceTax", true},
				{"CapitalGainsTax", true},
				{"QuebecIndependence", false},
				{"Bilingualism", true}
			}

		},
		{
			"GPC", new Dictionary<string, object>
			{
				{"WealthTax", true},
				{"FinancialLiteracyClasses", true},
				{"RehibilationPrograms", true},
				{"AffordableHousing", true},
				{"HighSpeedRail", true},
				{"MinimumWage", true},
				{"UnionFunding", true},
				{"RightToWork", false},
				{"UBI", true},
				{"SmallBuisnessSubsidies", true},
				{"PublicOwnershipOfMajorIndustries", true},
				{"WealthyTaxCuts", false},
				{"Deregulation", false},
				{"TransactionTax", true},
				{"CarbonTax", true},
				{"FreeTrade", false},
				{"PharmaCare", true},
				{"FreeMentalHealth", true},
				{"RareDeseaseResearch", true},
				{"DrugDecrim", true},
				{"DrugPriceCaps", true},
				{"LongTermCareFunding", true},
				{"UniversalHealthCare", true},
				{"FreeUniversityTuition", true},
				{"PublicSchoolFunding", true},
				{"SchoolVouchers", false},
				{"StudentLoanElim", true},
				{"NationalCurriculum", true},
				{"TeacherSalaryIncrease", true},
				{"STEMFunding", true},
				{"PrivateSchoolBan", false},
				{"SingleUsePlasticsBan", true},
				{"RenewableEnergyInvestements", true},
				{"FossileFuelPhaseOut", true},
				{"NationalParkExpansion", true},
				{"2050CarbonNeutrality", true},
				{"EVSubsidies", true},
				{"PolutionRegs", true},
				{"NuclearEnergy", false},
				{"FrackingBan", true},
				{"UniversalChildCare", true},
				{"GunControl", true},
				{"Abortion", true},
				{"GayMarriage", true},
				{"GenderNeutralRestrooms", true},
				{"ConversionTherapyBan", true},
				{"InformedConsent", true},
				{"LegalSexWork", true},
				{"PaidParentalLeave", true},
				{"IndigenousFunding", true},
				{"PoliceFundingIncrease", false},
				{"MandatoryBodyCams", true},
				{"CriminalJusticeReform", true},
				{"MinimumSentencing", false},
				{"DeathPenalty", false},
				{"PrivatePrisonBan", true},
				{"IncreasedSurveilance", false},
				{"OpenBorders", true},
				{"IncreasedFundingForImmigration", true},
				{"BorderWall", false},
				{"PathwayToCitizenship", true},
				{"MassDeportation", false},
				{"RefugeeResettlementProgram", true},
				{"PointsBasedImmigration", true},
				{"BanFromSpecific", false},
				{"PublicTransportPolicy", true},
				{"GovernmentHighSpeedInternet", true},
				{"HighwayExpansion", false},
				{"WaterInfrastructureUpgrades", true},
				{"UrbanRenewal", true},
				{"Withdrawl", true},
				{"IncreasedDefenseSpending", false},
				{"NATOSupport", false},
				{"EconomicSanctions", true},
				{"HumanatarianAid", true},
				{"ClimateTreaties", true},
				{"MilataryAlliances", false},
				{"TradeEmbargoes", false},
				{"AIResearchFunding", true},
				{"BigTechReg", true},
				{"FacialTechBan", true},
				{"InternetPrivacyLaws", true},
				{"GreenTechTaxCredit", true},
				{"UniversalInternet", true},
				{"CryptoAdoption", false},
				{"LowerIncomeTax", "Maintain"},
				{"MiddleIncomeTax", "Increase"},
				{"HighIncomeTax", "Increase"},
				{"FlatIncomeTax", false},
				{"CorprateTax", "Increase"},
				{"SalesTax", false},
				{"LuxuryGoodsTax", true},
				{"InherentenceTax", true},
				{"CapitalGainsTax", true},
				{"QuebecIndependence", false},
				{"Bilingualism", true}
			}

		},
		{
			"Bloc Quebecois", new Dictionary<string, object>
			{
    			{"WealthTax", true},
				{"FinancialLiteracyClasses", true},
				{"RehibilationPrograms", true},
				{"AffordableHousing", true},
				{"HighSpeedRail", true},
				{"MinimumWage", true},
				{"UnionFunding", true},
				{"RightToWork", false},
				{"UBI", false},
				{"SmallBuisnessSubsidies", true},
				{"PublicOwnershipOfMajorIndustries", false},
				{"WealthyTaxCuts", false},
				{"Deregulation", false},
				{"TransactionTax", true},
				{"CarbonTax", true},
				{"FreeTrade", false},
				{"PharmaCare", true},
				{"FreeMentalHealth", true},
				{"RareDeseaseResearch", true},
				{"DrugDecrim", true},
				{"DrugPriceCaps", true},
				{"LongTermCareFunding", true},
				{"UniversalHealthCare", true},
				{"FreeUniversityTuition", false},
				{"PublicSchoolFunding", true},
				{"SchoolVouchers", false},
				{"StudentLoanElim", false},
				{"NationalCurriculum", false},
				{"TeacherSalaryIncrease", true},
				{"STEMFunding", true},
				{"PrivateSchoolBan", false},
				{"SingleUsePlasticsBan", true},
				{"RenewableEnergyInvestements", true},
				{"FossileFuelPhaseOut", true},
				{"NationalParkExpansion", true},
				{"2050CarbonNeutrality", true},
				{"EVSubsidies", true},
				{"PolutionRegs", true},
				{"NuclearEnergy", false},
				{"FrackingBan", true},
				{"UniversalChildCare", true},
				{"GunControl", true},
				{"Abortion", true},
				{"GayMarriage", true},
				{"GenderNeutralRestrooms", true},
				{"ConversionTherapyBan", true},
				{"InformedConsent", true},
				{"LegalSexWork", true},
				{"PaidParentalLeave", true},
				{"IndigenousFunding", true},
				{"PoliceFundingIncrease", false},
				{"MandatoryBodyCams", true},
				{"CriminalJusticeReform", true},
				{"MinimumSentencing", false},
				{"DeathPenalty", false},
				{"PrivatePrisonBan", true},
				{"IncreasedSurveilance", false},
				{"OpenBorders", false},
				{"IncreasedFundingForImmigration", true},
				{"BorderWall", false},
				{"PathwayToCitizenship", true},
				{"MassDeportation", false},
				{"RefugeeResettlementProgram", true},
				{"PointsBasedImmigration", true},
				{"BanFromSpecific", false},
				{"PublicTransportPolicy", true},
				{"GovernmentHighSpeedInternet", true},
				{"HighwayExpansion", false},
				{"WaterInfrastructureUpgrades", true},
				{"UrbanRenewal", true},
				{"Withdrawl", false},
				{"IncreasedDefenseSpending", false},
				{"NATOSupport", false},
				{"EconomicSanctions", true},
				{"HumanatarianAid", true},
				{"ClimateTreaties", true},
				{"MilataryAlliances", false},
				{"TradeEmbargoes", false},
				{"AIResearchFunding", true},
				{"BigTechReg", true},
				{"FacialTechBan", true},
				{"InternetPrivacyLaws", true},
				{"GreenTechTaxCredit", true},
				{"UniversalInternet", true},
				{"CryptoAdoption", false},
				{"LowerIncomeTax", "Maintain"},
				{"MiddleIncomeTax", "Increase"},
				{"HighIncomeTax", "Increase"},
				{"FlatIncomeTax", false},
				{"CorprateTax", "Increase"},
				{"SalesTax", false},
				{"LuxuryGoodsTax", true},
				{"InherentenceTax", true},
				{"CapitalGainsTax", true},
				{"QuebecIndependence", true},
				{"Bilingualism", true}
			}

		},
		{
			"CPC", new Dictionary<string, object>
			{
				{"WealthTax", false},
				{"FinancialLiteracyClasses", true},
				{"RehibilationPrograms", true},
				{"AffordableHousing", false},
				{"HighSpeedRail", false},
				{"MinimumWage", false},
				{"UnionFunding", false},
				{"RightToWork", true},
				{"UBI", false},
				{"SmallBuisnessSubsidies", true},
				{"PublicOwnershipOfMajorIndustries", false},
				{"WealthyTaxCuts", true},
				{"Deregulation", true},
				{"TransactionTax", false},
				{"CarbonTax", false},
				{"FreeTrade", true},
				{"PharmaCare", false},
				{"FreeMentalHealth", false},
				{"RareDeseaseResearch", true},
				{"DrugDecrim", false},
				{"DrugPriceCaps", false},
				{"LongTermCareFunding", true},
				{"UniversalHealthCare", true},
				{"FreeUniversityTuition", false},
				{"PublicSchoolFunding", true},
				{"SchoolVouchers", true},
				{"StudentLoanElim", false},
				{"NationalCurriculum", true},
				{"TeacherSalaryIncrease", false},
				{"STEMFunding", true},
				{"PrivateSchoolBan", false},
				{"SingleUsePlasticsBan", false},
				{"RenewableEnergyInvestements", true},
				{"FossileFuelPhaseOut", false},
				{"NationalParkExpansion", false},
				{"2050CarbonNeutrality", false},
				{"EVSubsidies", false},
				{"PolutionRegs", false},
				{"NuclearEnergy", true},
				{"FrackingBan", false},
				{"UniversalChildCare", false},
				{"GunControl", false},
				{"Abortion", false},
				{"GayMarriage", false},
				{"GenderNeutralRestrooms", false},
				{"ConversionTherapyBan", true},
				{"InformedConsent", false},
				{"LegalSexWork", false},
				{"PaidParentalLeave", true},
				{"IndigenousFunding", false},
				{"PoliceFundingIncrease", true},
				{"MandatoryBodyCams", true},
				{"CriminalJusticeReform", false},
				{"MinimumSentencing", true},
				{"DeathPenalty", true},
				{"PrivatePrisonBan", false},
				{"IncreasedSurveilance", true},
				{"OpenBorders", false},
				{"IncreasedFundingForImmigration", false},
				{"BorderWall", false},
				{"PathwayToCitizenship", true},
				{"MassDeportation", true},
				{"RefugeeResettlementProgram", false},
				{"PointsBasedImmigration", true},
				{"BanFromSpecific", true},
				{"PublicTransportPolicy", false},
				{"GovernmentHighSpeedInternet", false},
				{"HighwayExpansion", true},
				{"WaterInfrastructureUpgrades", true},
				{"UrbanRenewal", true},
				{"Withdrawl", false},
				{"IncreasedDefenseSpending", true},
				{"NATOSupport", true},
				{"EconomicSanctions", true},
				{"HumanatarianAid", false},
				{"ClimateTreaties", false},
				{"MilataryAlliances", true},
				{"TradeEmbargoes", false},
				{"AIResearchFunding", true},
				{"BigTechReg", false},
				{"FacialTechBan", false},
				{"InternetPrivacyLaws", false},
				{"GreenTechTaxCredit", false},
				{"UniversalInternet", false},
				{"CryptoAdoption", true},
				{"LowerIncomeTax", "Decrease"},
				{"MiddleIncomeTax", "Maintain"},
				{"HighIncomeTax", "Decrease"},
				{"FlatIncomeTax", true},
				{"CorprateTax", "Decrease"},
				{"SalesTax", false},
				{"LuxuryGoodsTax", false},
				{"InherentenceTax", false},
				{"CapitalGainsTax", false},
				{"QuebecIndependence", false},
				{"Bilingualism", true}
			}

		},
		{
			"PPC", new Dictionary<string, object>
			{

				{"WealthTax", false},
				{"FinancialLiteracyClasses", true},
				{"RehibilationPrograms", false},
				{"AffordableHousing", false},
				{"HighSpeedRail", false},
				{"MinimumWage", false},
				{"UnionFunding", false},
				{"RightToWork", true},
				{"UBI", false},
				{"SmallBuisnessSubsidies", false},
				{"PublicOwnershipOfMajorIndustries", false},
				{"WealthyTaxCuts", true},
				{"Deregulation", true},
				{"TransactionTax", false},
				{"CarbonTax", false},
				{"FreeTrade", true},
				{"PharmaCare", false},
				{"FreeMentalHealth", false},
				{"RareDeseaseResearch", false},
				{"DrugDecrim", false},
				{"DrugPriceCaps", false},
				{"LongTermCareFunding", false},
				{"UniversalHealthCare", true},
				{"FreeUniversityTuition", false},
				{"PublicSchoolFunding", true},
				{"SchoolVouchers", true},
				{"StudentLoanElim", false},
				{"NationalCurriculum", false},
				{"TeacherSalaryIncrease", false},
				{"STEMFunding", false},
				{"PrivateSchoolBan", false},
				{"SingleUsePlasticsBan", false},
				{"RenewableEnergyInvestements", false},
				{"FossileFuelPhaseOut", false},
				{"NationalParkExpansion", false},
				{"2050CarbonNeutrality", false},
				{"EVSubsidies", false},
				{"PolutionRegs", false},
				{"NuclearEnergy", true},
				{"FrackingBan", false},
				{"UniversalChildCare", false},
				{"GunControl", false},
				{"Abortion", false},
				{"GayMarriage", false},
				{"GenderNeutralRestrooms", false},
				{"ConversionTherapyBan", false},
				{"InformedConsent", false},
				{"LegalSexWork", false},
				{"PaidParentalLeave", false},
				{"IndigenousFunding", false},
				{"PoliceFundingIncrease", true},
				{"MandatoryBodyCams", false},
				{"CriminalJusticeReform", false},
				{"MinimumSentencing", true},
				{"DeathPenalty", true},
				{"PrivatePrisonBan", false},
				{"IncreasedSurveilance", false},
				{"OpenBorders", false},
				{"IncreasedFundingForImmigration", false},
				{"BorderWall", true},
				{"PathwayToCitizenship", false},
				{"MassDeportation", true},
				{"RefugeeResettlementProgram", false},
				{"PointsBasedImmigration", true},
				{"BanFromSpecific", true},
				{"PublicTransportPolicy", false},
				{"GovernmentHighSpeedInternet", false},
				{"HighwayExpansion", true},
				{"WaterInfrastructureUpgrades", true},
				{"UrbanRenewal", false},
				{"Withdrawl", true},
				{"IncreasedDefenseSpending", true},
				{"NATOSupport", false},
				{"EconomicSanctions", false},
				{"HumanatarianAid", false},
				{"ClimateTreaties", false},
				{"MilataryAlliances", false},
				{"TradeEmbargoes", false},
				{"AIResearchFunding", true},
				{"BigTechReg", false},
				{"FacialTechBan", false},
				{"InternetPrivacyLaws", false},
				{"GreenTechTaxCredit", false},
				{"UniversalInternet", false},
				{"CryptoAdoption", true},
				{"LowerIncomeTax", "Decrease"},
				{"MiddleIncomeTax", "Decrease"},
				{"HighIncomeTax", "Decrease"},
				{"FlatIncomeTax", true},
				{"CorprateTax", "Decrease"},
				{"SalesTax", false},
				{"LuxuryGoodsTax", false},
				{"InherentenceTax", false},
				{"CapitalGainsTax", false},
				{"QuebecIndependence", false},
				{"Bilingualism", false}
			}
		}
	};

	private Vector4 LPCScore;
	private Vector4 NDPScore;
	private Vector4 GPCScore;
	private Vector4 BlocQuebecoisScore;
	private Vector4 CPCScore;
	private Vector4 PPCScore;
	private Dictionary<string, Vector4> PartyScores;

	private Dictionary<string, Dictionary<string, double>> SeparatePoliciesByCategory()
	{
		var separatedPolicies = new Dictionary<string, Dictionary<string, double>>
		{
			{"Social", new Dictionary<string, double>()},
			{"Economic", new Dictionary<string, double>()},
			{"Quebec", new Dictionary<string, double>()},
			{"Environmental", new Dictionary<string, double>()}
		};

		foreach (var policy in PolicyWeights)
		{
			string policyName = policy.Key;
			double weight = policy.Value;

			if (PolicyCategories.ContainsKey(policyName))
			{
				string category = PolicyCategories[policyName];
				if (separatedPolicies.ContainsKey(category))
				{
					separatedPolicies[category][policyName] = weight;
				}
			}
		}

		return separatedPolicies;
	}

	private Vector4 CalculateScores(Dictionary<string, object> currentPolicyStates)
	{
		// Separate policies dynamically
		var separatedPolicies = SeparatePoliciesByCategory();

		float socialScore = 0;
		float economicScore = 0;
		float quebecScore = 0;
		float environmentalScore = 0;

		foreach (var category in separatedPolicies)
		{
			foreach (var policy in category.Value)
			{
				string policyName = policy.Key;
				double weight = policy.Value;

				// Check if the policy state exists and calculate its contribution
				if (currentPolicyStates.ContainsKey(policyName))
				{
					object state = currentPolicyStates[policyName];
					float contribution = 0;

					if (state is bool boolState)
					{
						contribution = boolState ? (float)weight : (float)-weight;
					}
					else if (state is string stringState)
					{
						contribution = stringState switch
						{
							"Increase" => (float)weight,
							"Maintain" => 0,
							"Decrease" => (float)-weight,
							_ => 0
						};
					}

					// Add contribution to the appropriate score
					switch (category.Key)
					{
						case "Social":
							socialScore += contribution;
							break;
						case "Economic":
							economicScore += contribution;
							break;
						case "Quebec":
							quebecScore += contribution;
							break;
						case "Environmental":
							environmentalScore += contribution;
							break;
					}
				}
			}
		}

		return new Vector4(socialScore, economicScore, quebecScore, environmentalScore);
	}

	public void _on_policy_score_pressed()
	{
		Vector4 Scores = CalculateScores(GetCurrentPolicyState());
		EconomicScoreLabel.Text = $"Economic Score: {Math.Round(Scores.y, 3)}"; // Y is the Economic Score
		SocialScoreLabel.Text = $"Social Score: {Math.Round(Scores.x, 3)}"; // X is the Social Score
		QuebecScoreLabel.Text = $"Quebec Score: {Math.Round(Scores.z, 3)}"; // Z is the Quebec Score
		EnvironmentalScoreLabel.Text = $"Environmental Score: {Math.Round(Scores.w, 3)}"; // W is the Environmental Score
		Dictionary<string, double> Deviations = new Dictionary<string, double>();
		foreach (var party in PartyScores)
		{
			float deviation = Math.Abs(Scores.Distance(party.Value) / party.Value.Magnitude()); // We want the absolute value of the deviation, so that negative deviations are treated the same as positive deviations
			Deviations[party.Key] = deviation;
			GD.Print(party.Key + " Deviation: " + deviation);
		}
		if (Scores.z == -10)
		{
			Deviations["Bloc Quebecois"] = 0; // If the player is a Quebec Nationalist, then the Bloc Quebecois is the suggested party
		}
		// The party with the smallest deviation is the suggested party, so the minimum value in the dictionary. We want the abesolute value of the deviation, so that negative deviations are treated the same as positive deviations
		string SuggestedParty = Deviations.MinBy(x => x.Value).Key;
		SuggestedPartyLabel.Text = "Suggested Party: " + SuggestedParty;
	}

	private Dictionary<string, object> GetCurrentPolicyState()
	{
		Dictionary<string, object> policyState = new Dictionary<string, object>();
		foreach (Node node in GetTree().GetNodesInGroup("PolicyGroup"))
		{
			if (node is CheckBox checkBox)
			{
				string PolicyName = checkBox.GetParent().GetParent().Name;
				if (!policyState.ContainsKey(PolicyName))
				{
					if (checkBox.Text == "Yes" || checkBox.Text == "No")
					{
						policyState[PolicyName] = checkBox.ButtonPressed && checkBox.Text == "Yes";

					}
					else  // Maintain, Decrease & Increase Cases
					{
						if (checkBox.ButtonPressed)
						{
							policyState[PolicyName] = checkBox.Text;
						}
					}
				}
			}
		}
		GD.Print("Current Policy States: ");
		foreach (var state in policyState)
		{
			GD.Print(state.Key + ": " + state.Value);
		}
		return policyState;
	}

    public override void _Ready()
    {
		EnvironmentalScoreLabel = GetNode<Label>("../EnvironmentalScore");
		SocialScoreLabel = GetNode<Label>("../SocialScore");
		EconomicScoreLabel = GetNode<Label>("../EconomicScore");
		QuebecScoreLabel = GetNode<Label>("../QuebecScore");
		SuggestedPartyLabel = GetNode<Label>("../SuggestedParty");
		LPCScore = CalculateScores(PartyPolicies["LPC"]);
		NDPScore = CalculateScores(PartyPolicies["NDP"]);
		GPCScore = CalculateScores(PartyPolicies["GPC"]);
		BlocQuebecoisScore = CalculateScores(PartyPolicies["Bloc Quebecois"]);
		CPCScore = CalculateScores(PartyPolicies["CPC"]);
		PPCScore = CalculateScores(PartyPolicies["PPC"]);
		PartyScores = new Dictionary<string, Vector4>
		{
			{"LPC", LPCScore},
			{"NDP", NDPScore},
			{"GPC", GPCScore},
			{"Bloc Quebecois", BlocQuebecoisScore},
			{"CPC", CPCScore},
			{"PPC", PPCScore}
		};
		GD.Print("Party Scores: ");
		foreach (var party in PartyScores)
		{
			GD.Print(party.Key + ": " + party.Value.x + ", " + party.Value.y + ", " + party.Value.z + ", " + party.Value.w);
		}
		
    }


}

public class Vector4
{
	public float x; //  Social, more negative means more progressive, more positive means more conservative
	public float y; // Economic, more negative means more left, more positive means more right
	public float z; // Quebec Independence, more negative means more Nationalist, more positive means more Federalist
	public float w; // Environmental, more negative means more pro-environment, more positive means more pro-industry

	public Vector4(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

	public float Dot(Vector4 other)
	{
		return x * other.x + y * other.y + z * other.z + w * other.w; // We will use this to calculate the deviation from the players score relative to another score, like a politcal party
	}

	public float Distance(Vector4 other)
	{
		return (this - other).Magnitude(); // We will use this to calculate the deviation from the players score relative to another score, like a politcal party
	}

	public static Vector4 operator -(Vector4 a, Vector4 b)
	{
		return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
	}

	public float Magnitude()
	{
		return Mathf.Sqrt(x * x + y * y + z * z + w * w);
	}

}