// test2.js
import { Account } from "./test1.js";

class BankProcess extends Account {
    constructor(userId, userName, accountId, balance = 0) {
        super(userId, userName, accountId, balance);
    }

    depositAmount(amount) {
        if (amount > 0) {
            this.balance += amount;
            console.log(`Deposited: $${amount}. New Balance: $${this.balance}`);
        } else {
            console.log("Invalid deposit amount.");
        }
    }

    withdrawAmount(amount) {
        if (amount > 0 && amount <= this.balance) {
            this.balance -= amount;
            console.log(`Withdrawn: $${amount}. Remaining Balance: $${this.balance}`);
        } else {
            console.log("Insufficient funds or invalid withdrawal amount.");
        }
    }

    checkBalance() {
        console.log(`Account Balance: $${this.balance}`);
        return this.balance;
    }
}

const user1 = Account.createAccount(101, "Dharani", 987654);
console.log(user1.getAccountDetails());
console.log(Account.deleteAccount(101));
const bank1 = new BankProcess(101, "Dharani", 987654, 500);
bank1.depositAmount(300);
bank1.withdrawAmount(200);
bank1.checkBalance();
