import { PrintsData } from "../../../components/prints/Shared/model";

export class TypesData
{
    id: number;
    title: string;
    prints: PrintsData[] = [];
    createdAt: Date;
    createdBy: number;
    updatedAt: Date;
    updatedBy: number;
    deletedAt: Date;
    deletedBy: number;
}

