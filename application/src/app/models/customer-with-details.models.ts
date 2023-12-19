import { TransactionModel } from "./transaction.model";

export class CustomerWithDetailsModel {
    name: string;

    surname: string;

    balance: number;

    transactions: TransactionModel[];
}