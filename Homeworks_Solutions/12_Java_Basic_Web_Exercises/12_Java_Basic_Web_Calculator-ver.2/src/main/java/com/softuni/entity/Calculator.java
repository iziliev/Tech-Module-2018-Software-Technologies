package com.softuni.entity;

public class Calculator {
    private double leftOperand;
    private double rightOperand;
    private String operator;

    public Calculator(double leftOperand, double rightOperand, String operator) {
        this.leftOperand = leftOperand;
        this.rightOperand = rightOperand;
        this.operator = operator;
    }

    public double getLeftOperand() {
        return leftOperand;
    }

    public void setLeftOperand(double leftOperand) {
        this.leftOperand = leftOperand;
    }

    public double getRightOperand() {
        return rightOperand;
    }

    public void setRightOperand(double rightOperand) {
        this.rightOperand = rightOperand;
    }

    public String getOperator() {
        return operator;
    }

    public void setOperator(String operator) {
        this.operator = operator;
    }

    public double calculateResult() {

        double result;
		String errorMsg = "";
        switch (this.operator){

            case "+":
                result = this.leftOperand + this.rightOperand;
                break;
				
            case "-":
                result = this.leftOperand - this.rightOperand;
                break;
				
            case "*":
                result = this.leftOperand * this.rightOperand;
                break;
				
            case "/":
                result = this.leftOperand / this.rightOperand;
                break;
				
			case "^":
                result = Math.pow(this.leftOperand, this.rightOperand);
                break;
				
			case "%":
			    result = this.leftOperand % this.rightOperand;
                break;

            case "OR":
                result = (int)(this.leftOperand) | (int)(this.rightOperand);
                break;

            case "AND":
                result = (int)(this.leftOperand) & (int)(this.rightOperand);
                break;

            case "XOR":
                result = (int)(this.leftOperand) ^ (int)(this.rightOperand);
                break;

            default:
                result = 0;
                break;
        }
        return result;
    }
}
