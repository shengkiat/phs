import static org.junit.Assert.*;

import java.io.File;
import java.util.List;
import java.util.Random;

import org.junit.Before;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.By.ById;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxBinary;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.firefox.FirefoxProfile;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;

public class FirstTest {
	
	WebDriver driver; 
	long sleepTime = 2000;
	long sleepShort = 1000; 
	String formBuilderLocation = "form-builder-frame-8";
	String phsURLPath = "https://medworks.nus.edu.sg/phs";
	
	@Before
	public void prepareTest() {
		System.setProperty("webdriver.gecko.driver", "D:\\Apps\\GeckoDriver\\geckodriver.exe");
		driver = new FirefoxDriver();
		driver.navigate().to(phsURLPath);
		String strPageTitle = driver.getTitle();
		System.out.println(strPageTitle);
	}



	@Test
	public void test() throws InterruptedException {

		
		while (true) {
			testLogin();
			CharSequence nric = generateNRIC();
			testCreateUser(nric);
			testMegaSortingStation();
			driver.navigate().to(phsURLPath);
			// testLogout();
		}
		// testGoToHistoryTaking(nric);
		// testRegistrationForm(nric);	
			//testConsentForm(nric); 
			
			
			 
			
	}
	
	
	public String generateNRIC() {
		
		int[] checkWeight = new int[7]; 
		String[] checkAlpha = new String[12];
		int[] nric = new int[7];
 		
		checkWeight[0] = 2;
		checkWeight[1] = 7;
		checkWeight[2] = 6;
		checkWeight[3] = 5;
		checkWeight[4] = 4;
		checkWeight[5] = 3;
		checkWeight[6] = 2;
		
		checkAlpha[0] = "1"; 
		checkAlpha[1] = "A"; 
		checkAlpha[2] = "B"; 
		checkAlpha[3] = "C"; 
		checkAlpha[4] = "D"; 
		checkAlpha[5] = "E"; 
		checkAlpha[6] = "F"; 
		checkAlpha[7] = "G"; 
		checkAlpha[8] = "H"; 
		checkAlpha[9] = "I"; 
		checkAlpha[10] = "Z"; 
		checkAlpha[11] = "J";
		
		int totalSum = 0; 
		
		for (int i = 0; i < 7; i++) {
			Random randomGenerator = new Random();
			int randomInt = randomGenerator.nextInt(10);
			nric[i] = randomInt; 
			totalSum = totalSum + randomInt * checkWeight[i]; 			
		}
		
		int modNumber = totalSum % 11; 
		int checkAlphaNumber = 11 - modNumber; 
		String checkSumAlpha = checkAlpha[checkAlphaNumber]; 
		String nricString = "S";
		
		for (int j = 0; j < 7; j ++) {
			nricString = nricString + nric[j];
		}
		
		nricString = nricString + checkSumAlpha; 
		
		return nricString; 
	}
	
	public void testGoToHistoryTaking(CharSequence nric) throws InterruptedException {
		WebElement liveEventLink = driver.findElement(By.ById.id("19"));
		liveEventLink.click();
		Thread.sleep(sleepTime);
		
		
		
		WebElement othersTab = driver.findElement(By.ByLinkText.linkText("8 - Others"));
		othersTab.click();
		Thread.sleep(sleepTime);
		Thread.sleep(sleepTime);
		
		driver.switchTo().frame(driver.findElement(By.ById.id("form-builder-frame-8")));
		Thread.sleep(sleepShort);
		
		
		WebElement nameField = driver.findElement(By.ByName.name("SubmitFields[1].TextArea")); 
		nameField.sendKeys("Others field text, Name NRIC " + nric);
		Thread.sleep(sleepShort);
		
		WebElement submitButton = driver.findElement(By.className("save-button blue"));
		submitButton.click();

	}
	

	public void testConsentForm(CharSequence nric) throws InterruptedException {
		WebElement liveEventLink = driver.findElement(By.ByLinkText.linkText("1 - PHS 2017 Screening Consent Form"));
		liveEventLink.click();
		Thread.sleep(sleepTime);
		
		driver.switchTo().frame(driver.findElement(By.ById.id(formBuilderLocation)));
		Thread.sleep(sleepShort);
		
		

		JavascriptExecutor jse = (JavascriptExecutor)driver;
		jse.executeScript("scroll(0, 10000)");
		
		Thread.sleep(sleepTime);
		

		List<WebElement> listOfRadio10 = driver.findElements(By.ByName.name("SubmitFields[10].RadioButton"));	
		System.out.println(listOfRadio10.size());
		for (int i = 0; i < listOfRadio10.size(); i++) {	
			System.out.println(listOfRadio10.get(i).getAttribute("value"));
			if(listOfRadio10.get(i).getAttribute("value").contains("I Consent")) {
				listOfRadio10.get(i).click();
			}
		}
		
		List<WebElement> listOfRadio11 = driver.findElements(By.ByName.name("SubmitFields[11].RadioButton"));	
		System.out.println(listOfRadio11.size());
		for (int i = 0; i < listOfRadio11.size(); i++) {	
			System.out.println(listOfRadio11.get(i).getAttribute("value"));
			if(listOfRadio11.get(i).getAttribute("value").contains("Yes")) {
				listOfRadio11.get(i).click();
			}
		}
		
		List<WebElement> listOfRadio12 = driver.findElements(By.ByName.name("SubmitFields[12].RadioButton"));	
		System.out.println(listOfRadio12.size());
		for (int i = 0; i < listOfRadio12.size(); i++) {	
			System.out.println(listOfRadio12.get(i).getAttribute("value"));
			if(listOfRadio12.get(i).getAttribute("value").contains("Yes")) {
				listOfRadio12.get(i).click();
			}
		}


	}
	
	public void testRegistrationForm(CharSequence nric) throws InterruptedException {
		WebElement liveEventLink = driver.findElement(By.ByLinkText.linkText("0 - Registration Form"));
		liveEventLink.click();
		Thread.sleep(sleepTime);
		
		//JavascriptExecutor jse = (JavascriptExecutor)driver;


		driver.switchTo().frame(driver.findElement(By.ById.id(formBuilderLocation)));
		Thread.sleep(sleepShort);
		
		WebElement nameField = driver.findElement(By.ByName.name("SubmitFields[2].TextBox")); 
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", nameField);
		nameField.sendKeys("Name NRIC " + nric);
		Thread.sleep(sleepShort);


		List<WebElement> listOfGender = driver.findElements(By.ByName.name("SubmitFields[3].RadioButton"));	
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", listOfGender);
		for(WebElement element: listOfGender) {
			if(element.getAttribute("value").contains("Male 男性")) {
				element.click();
			}
		}
		Thread.sleep(sleepShort);	

	
		List<WebElement> listOfSalutation = driver.findElements(By.ByName.name("SubmitFields[4].RadioButton"));	
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", listOfSalutation);
		for(WebElement element: listOfSalutation) {
			if(element.getAttribute("value").contains("Ms")) {
				element.click();
			}
		}
		Thread.sleep(sleepShort);

		
		List<WebElement> listOfCitizenship = driver.findElements(By.ByName.name("SubmitFields[7].RadioButton"));
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", listOfCitizenship);
		for(WebElement element: listOfCitizenship) {
			System.out.println(element.getAttribute("value"));
			if(element.getAttribute("value").contains("Singapore Citizen")) {
				element.click();
			}
		}
		Thread.sleep(sleepShort);
	
	
		List<WebElement> listOfRace = driver.findElements(By.ByName.name("SubmitFields[8].RadioButton"));	
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", listOfRace);
		for(WebElement element: listOfRace) {
			System.out.print(element.getAttribute("value"));
			if(element.getAttribute("value").contains("Chinese")) {
				element.click();
			}
		}
		Thread.sleep(sleepShort);
		
		
		
		List<WebElement> listOfMarital = driver.findElements(By.ByName.name("SubmitFields[10].RadioButton"));		
		for (int i = 0; i < listOfMarital.size(); i++) {	
			if(listOfMarital.get(i).getAttribute("value").equals("Married")) {
				listOfMarital.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		List<WebElement> listOfLang = driver.findElements(By.ByName.name("SubmitFields[12].RadioButton"));		
		for (int i = 0; i < listOfLang.size(); i++) {	
			if(listOfLang.get(i).getAttribute("value").equals("Mandarin")) {
				listOfLang.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		WebElement postal = driver.findElement(By.ByName.name("SubmitFields[15].ZipCode")); 
		postal.sendKeys("100001");
		Thread.sleep(sleepShort);
		
		WebElement postalSearch = driver.findElement(By.ById.id("btnAddressSubmit"));
		postalSearch.click();
		Thread.sleep(sleepShort);


		
		WebElement unit = driver.findElement(By.ByName.name("SubmitFields[15].Unit")); 
		unit.sendKeys(nric);
		Thread.sleep(sleepShort);

		List<WebElement> listOfHousing = driver.findElements(By.ByName.name("SubmitFields[16].RadioButton"));		
		for (int i = 0; i < listOfHousing.size(); i++) {	
			if(listOfHousing.get(i).getAttribute("value").equals("HDB 4-Room Flat")) {
				listOfHousing.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		WebElement homeNumber = driver.findElement(By.ByName.name("SubmitFields[18].TextBox")); 
		homeNumber.sendKeys("HomeNum: " + nric);
		Thread.sleep(sleepShort);
		
		WebElement mobileNumber = driver.findElement(By.ByName.name("SubmitFields[19].TextBox")); 
		mobileNumber.sendKeys("MobileNum: " + nric);
		Thread.sleep(sleepShort);

		WebElement NOKHomeNumber = driver.findElement(By.ByName.name("SubmitFields[21].TextBox")); 
		NOKHomeNumber.sendKeys("NOK: " + nric);
		Thread.sleep(sleepShort);
		
		WebElement NOKmobileNumber = driver.findElement(By.ByName.name("SubmitFields[22].TextBox")); 
		NOKmobileNumber.sendKeys("NOKMobile: " + nric);
		Thread.sleep(sleepShort);

		WebElement NOKrelationship = driver.findElement(By.ByName.name("SubmitFields[23].TextBox")); 
		NOKrelationship.sendKeys("NOKRelationship: " + nric);
		Thread.sleep(sleepShort);


		List<WebElement> listOfEducation = driver.findElements(By.ByName.name("SubmitFields[24].RadioButton"));		
		for (int i = 0; i < listOfEducation.size(); i++) {	
			if(listOfEducation.get(i).getAttribute("value").equals("Secondary")) {
				listOfEducation.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		List<WebElement> listOfCHAS = driver.findElements(By.ByName.name("SubmitFields[26].RadioButton"));		
		for (int i = 0; i < listOfCHAS.size(); i++) {	
			if(listOfCHAS.get(i).getAttribute("value").equals("No")) {
				listOfCHAS.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		List<WebElement> listOfPioneer = driver.findElements(By.ByName.name("SubmitFields[27].RadioButton"));		
		for (int i = 0; i < listOfPioneer.size(); i++) {	
			if(listOfPioneer.get(i).getAttribute("value").equals("Yes")) {
				listOfPioneer.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		List<WebElement> listOfEligibility = driver.findElements(By.ById.id("SubmitFields[29].CheckBox"));		
		System.out.println("Size: " + listOfEligibility.size());
		for (int i = 0; i < listOfEligibility.size(); i++) {	
			System.out.println(listOfEligibility.get(i).getAttribute("value"));
			if(listOfEligibility.get(i).getAttribute("value").equals("(1) Fasted for at least 10 hours 十个小时内没有吃东西或喝饮料")) {
				listOfEligibility.get(i).click();
			}
			if(listOfEligibility.get(i).getAttribute("value").equals("(3) Have not been previously diagnosed with diabetes mellitus OR hyperlipidemia OR hypertension 没有被诊断患有高血压 或 高血脂 或 高血压")) {
				listOfEligibility.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		List<WebElement> listOfHtReport = driver.findElements(By.ByName.name("SubmitFields[32].RadioButton"));		
		for (int i = 0; i < listOfHtReport.size(); i++) {	
			if(listOfHtReport.get(i).getAttribute("value").equals("English and Malay")) {
				listOfHtReport.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		List<WebElement> listOfClinic = driver.findElements(By.ByName.name("SubmitFields[33].RadioButton"));		
		for (int i = 0; i < listOfClinic.size(); i++) {	
			if(listOfClinic.get(i).getAttribute("value").equals("Ayer Rajah - Pandan Clinic (Blk 415)")) {
				listOfClinic.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		//postalSearch.submit();
		
	}
	
	public void testMegaSortingStation() throws InterruptedException {
		WebElement liveEventLink = driver.findElement(By.ByLinkText.linkText("Mega Sorting Station"));
		liveEventLink.click();
		Thread.sleep(sleepTime);
		
		JavascriptExecutor jse = (JavascriptExecutor)driver;
		
		driver.switchTo().frame(driver.findElement(By.ById.id("form-builder-frame-8")));
		Thread.sleep(sleepShort);

		List<WebElement> listOfEligibility = driver.findElements(By.ById.id("IsActive"));		
		System.out.println("Size: " + listOfEligibility.size());
		for (int i = 0; i < listOfEligibility.size(); i++) {	
			System.out.println(listOfEligibility.get(i).getAttribute("value"));

			((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", listOfEligibility.get(i));
			
			if(listOfEligibility.get(i).isEnabled() == true) {
			listOfEligibility.get(i).click();
			}
		}
		Thread.sleep(sleepShort);
		
		
		
	}
	
	public void testCreateUser(CharSequence nricString) throws InterruptedException {
		WebElement liveEventLink = driver.findElement(By.ByLinkText.linkText("Event"));
		liveEventLink.click();
		Thread.sleep(sleepTime);
		
		WebElement nric = driver.findElement(By.ById.id("Nric"));
		nric.sendKeys(nricString);
		
		WebElement searchButton = driver.findElement(By.ByName.name("BtnSearch")); 
		searchButton.click();
		Thread.sleep(1000);
		
		WebElement yesRegisterLink = driver.findElement(By.ByLinkText.linkText("Yes, Register"));
		yesRegisterLink.click();
		Thread.sleep(sleepTime);
	}
	
	public void testLogin() throws InterruptedException {
		WebElement loginUser = driver.findElement(By.ById.id("Username"));
		loginUser.sendKeys("admin1");
		
		WebElement password = driver.findElement(By.ById.id("Password")); 
		password.sendKeys("1qazxsw2!@QW");
		
		WebDriverWait wait = new WebDriverWait(driver, sleepTime); 
				
		WebElement submitButton = driver.findElement(By.ById.id("BtnLogin")); 
		submitButton.click();
		Thread.sleep(sleepTime);
	}
	
	public void testLogout() throws InterruptedException {
		WebElement profileButton = driver.findElement(By.ByXPath.xpath("//button[@text='Super Admin']"));
		profileButton.click();
		profileButton.wait(sleepTime);
		
		WebElement logout = driver.findElement(By.ByLinkText.linkText("Logout"));
		logout.click();
		logout.wait(); 
	}
	
	
	public void testMenuItems() throws InterruptedException {
		WebElement userLink = driver.findElement(By.ByLinkText.linkText("User Management"));
		userLink.click();
		Thread.sleep(sleepTime);
		
		WebElement eventLink = driver.findElement(By.ByLinkText.linkText("Event Management"));
		eventLink.click();
		Thread.sleep(sleepTime);
		
		WebElement formLink = driver.findElement(By.ByLinkText.linkText("Form Management"));
		formLink.click();
		Thread.sleep(sleepTime);
		
		WebElement stdRefLink = driver.findElement(By.ByLinkText.linkText("Standard Reference"));
		stdRefLink.click();
		Thread.sleep(sleepTime);
		
		WebElement pastEventLink = driver.findElement(By.ByLinkText.linkText("Past Event"));
		pastEventLink.click();
		Thread.sleep(sleepTime);
		
		WebElement liveEventLink = driver.findElement(By.ByLinkText.linkText("Event"));
		liveEventLink.click();
		Thread.sleep(sleepTime);
	}

}
