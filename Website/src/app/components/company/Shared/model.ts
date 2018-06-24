import { PrintsData } from "../../../components/prints/Shared/model";

export class CompanyData
{
    id: number;
    title: string;
    budget: string;
    notes: string;
    prints: PrintsData[] = [];
    createdAt: Date;
    createdBy: number;
    updatedAt: Date;
    updatedBy: number;
    deletedAt: Date;
    deletedBy: number;
}

