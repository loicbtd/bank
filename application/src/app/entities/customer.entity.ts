import { CurrentAccountEntity } from "./current-account.entity";

export class CustomerEntity {
    id: string;

    customerId: string;

    name: string;

    surname: string;

    currentAccount: CurrentAccountEntity;
}
