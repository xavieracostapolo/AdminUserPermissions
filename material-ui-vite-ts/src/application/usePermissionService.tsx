import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { usePermissionRepository } from '../infraestructure'

const key: any = 'permissions'

export const usePermissionService = () => {
    const queryClient = useQueryClient()
    const permissionRepository = usePermissionRepository()
    
    const useGetPermission = () => {
        return useQuery({ queryKey: [key], queryFn: permissionRepository.get })
    }

    const useCreatePermission = () => {
        return useMutation({
            mutationFn: permissionRepository.create,
            onSuccess(data, variables, context) {
                queryClient.invalidateQueries();
            },
        })
    }

    return {
        useGetPermission,
        useCreatePermission,
    }
}
