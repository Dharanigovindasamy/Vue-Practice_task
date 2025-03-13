// test1.js

export class UserClass {
    constructor(userId, userName) {
        this.userId = userId;
        this.userName = userName;
    }

    getUserDetails() {
        return `User ID: ${this.userId}, Name: ${this.userName}`;
    }
}

export class Account extends UserClass {
    constructor(userId, userName, accountId, balance = 0) {
        super(userId, userName);
        this.accountId = accountId;
        this.balance = balance;
    }

    static createAccount(userId, userName, accountId) {
        console.log(`Account created for ${userName} with Account ID: ${accountId}`);
        return new Account(userId, userName, accountId);
    }

    static deleteAccount(accountId) {
        console.log(`Account ${accountId} deleted successfully.`);
        return null; 
    }

    getAccountDetails() {
        return `Account ID: ${this.accountId}, User: ${this.userName}, Balance: $${this.balance}`;
    }
}
