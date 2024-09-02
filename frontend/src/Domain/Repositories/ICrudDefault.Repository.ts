export default interface ICrudDefault{
    GetAll(filter: object): Promise<any>;

    GetById(id: number): Promise<any>;

    Create(entity: any): Promise<any>;

    Update(entity: any): Promise<any>;

    Delete(id: number): Promise<any>;
}