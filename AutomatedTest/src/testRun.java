import java.util.Random;

public class testRun {

	public static void main(String[] args) {

		Random randomGenerator = new Random();
		int randomInt = randomGenerator.nextInt(10000000);
		System.out.println(randomInt);

	}

	
	public void nricGenerator() {
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
		
		System.out.print(nricString);
	}
}
