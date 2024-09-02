export default interface IAuthDefault {
    Authenticate(email: string, password: string): Promise<any>;
}