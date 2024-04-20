import { Permission } from '../domain'
import { apiClient } from './ApiClient'

export const usePermissionRepository = () => {
    const get = async (): Promise<Permission[]> => {
        return (await apiClient.get<Permission[]>('/Permissions')).data
    }

    const create = async (permission: Omit<Permission, 'id'>) => {
        const response = await apiClient.post<any>('/Permissions', permission)
        return response.data
    }

    const update = async (id: any, permission: Permission) => {
        const response = await apiClient.put<any>(
            `/Permissions/${id}`,
            permission
        )
        return response.data
    }

    const deleteById = async (id: any) => {
        const response = await apiClient.delete<any>(`/Permissions/${id}`)
        return response.data
    }

    return {
        create,
        get,
        deleteById,
        update,
    }
}
